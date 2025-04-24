using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;

namespace Compiler;
public class SimpleCompiler
{
	public static string HandleSubmission(string code)
	{
		string path;

		var compileErrors = CompileSubmission(code, out path);

		if (!string.IsNullOrEmpty(compileErrors))
		{
			return "[Compilation Failed]\n" + compileErrors;
		}

		string output = RunSubmission(path);

		Cleanup(path);

		return output;
	}

	private static string? CompileSubmission(string code, out string path)
	{
		path = Path.Combine(Path.GetTempPath(), $"submission_{Guid.NewGuid()}.exe");

		var syntaxTree = CSharpSyntaxTree.ParseText(code);

		var refs = AppDomain.CurrentDomain.GetAssemblies()
			.Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
			.Select(a => MetadataReference.CreateFromFile(a.Location));

		var compilation = CSharpCompilation.Create(
			Path.GetFileNameWithoutExtension(path),
			new[] { syntaxTree },
			refs,
			new CSharpCompilationOptions(OutputKind.ConsoleApplication)
		);

		var result = compilation.Emit(path);

		if (!result.Success)
		{
			var errors = result.Diagnostics
				.Where(d => d.Severity == DiagnosticSeverity.Error)
				.Select(d => d.ToString());
			return string.Join("\n", errors);
		}

		return null;
	}

	private static string RunSubmission(string exePath)
	{
		var psi = new ProcessStartInfo
		{
			FileName = exePath,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		using var process = new Process { StartInfo = psi };
		process.Start();

		string output = process.StandardOutput.ReadToEnd();
		string error = process.StandardError.ReadToEnd();
		process.WaitForExit();

		return string.IsNullOrWhiteSpace(error) ? output : "[Runtime Error]\n" + error;
	}

	private static void Cleanup(string path)
	{
		try { if (File.Exists(path)) File.Delete(path); } catch { }
	}
}
