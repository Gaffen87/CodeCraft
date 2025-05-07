using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;

namespace Compiler;
public partial class SimpleCompiler
{
	public static async Task<Dictionary<bool, string>> CodeRunner(Dictionary<string, string> codeFiles)
	{
		string tempProjectPath = Path.Combine(Path.GetTempPath(), "CSharpExecution");
		Directory.CreateDirectory(tempProjectPath);
		Directory.GetFiles(tempProjectPath).ToList().ForEach(System.IO.File.Delete);

		foreach (var item in codeFiles)
		{
			string filePath = Path.Combine(tempProjectPath, item.Key);
			await System.IO.File.WriteAllTextAsync(filePath, item.Value);
		}

		return CompileAndRun(tempProjectPath);
	}

	private static Dictionary<bool, string> CompileAndRun(string path)
	{
		var sourceFiles = Directory.GetFiles(path, "*.cs");
		var syntaxTrees = sourceFiles.Select(file
			=> CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(file))).ToList();

		var references = new List<MetadataReference>
		{
			MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
			MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
			MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location)
		};

		var compilation = CSharpCompilation.Create("DynamicProject")
			.WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
			.AddReferences(references)
			.AddSyntaxTrees(syntaxTrees);

		using var ms = new MemoryStream();
		var emitResult = compilation.Emit(ms);

		if (!emitResult.Success)
		{
			var errors = string.Join("\n", emitResult.Diagnostics
				.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
				.Select(diagnostic => diagnostic.ToString()));

			return new()
			{
				{ false, $"Compilation failed:\n{errors}" }
			};
		}

		ms.Seek(0, SeekOrigin.Begin);
		var assembly = Assembly.Load(ms.ToArray());

		var outputBuilder = new StringBuilder();
		using (var writer = new StringWriter(outputBuilder))
		{
			Console.SetOut(writer);
			var type = assembly.GetType("Program");
			var method = type?.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);

			if (method != null)
				method.Invoke(null, null);
		}

		return new()
		{
			{ true, outputBuilder.ToString() }
		};
	}
}
