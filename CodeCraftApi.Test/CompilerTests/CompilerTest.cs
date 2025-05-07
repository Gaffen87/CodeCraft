using Compiler;

namespace CodeCraftApi.Test.CompilerTests;
public class CompilerTest
{
	[Fact]
	public async Task ShouldReturnSuccessOnCompilation()
	{
		var input = new Dictionary<string, string>
		{
			{ "Program.cs", "using System;class Program{public static void Main(){Console.WriteLine(Helper.GetMessage());}}" },
			{ "Helper.cs", "using System;public class Helper{public static string GetMessage(){return \"Hello, World!\";}}" }
		};

		var result = await SimpleCompiler.CodeRunner(input);

		Assert.NotNull(result);
		Assert.True(result.Keys.First());
	}

	[Fact]
	public async Task ShouldReturnFalseOnCompilationError()
	{
		var input = new Dictionary<string, string>
		{
			{ "Program.cs", "class Program{public static void Main(){Console.WriteLine(Helper.GetMessage());}}" },
			{ "Helper.cs", "public class Helper{public static string GetMessage(){return \"Hello, World!\";}}" }
		};

		var result = await SimpleCompiler.CodeRunner(input);

		Assert.NotNull(result);
		Assert.False(result.Keys.First());
	}

	[Fact]
	public async Task ShouldReturnValidResult()
	{
		var input = new Dictionary<string, string>
		{
			{ "Program.cs", "using System;class Program{public static void Main(){Console.WriteLine(Helper.GetMessage());}}" },
			{ "Helper.cs", "using System;public class Helper{public static string GetMessage(){return \"Hello, World!\";}}" }
		};

		var result = await SimpleCompiler.CodeRunner(input);

		Assert.NotNull(result);
		Assert.Equal("Hello, World!\r\n", result.Values.First());
	}

	[Fact]
	public async Task ShouldExplainErrorsCorrectlyOnFail()
	{
		var input = new Dictionary<string, string>
		{
			{ "Program.cs", "class Program{public static void Main(){Console.WriteLine(Helper.GetMessage());}}" },
			{ "Helper.cs", "using System;public class Helper{public static string GetMessage(){return \"Hello, World!\";}}" }
		};

		var result = await SimpleCompiler.CodeRunner(input);

		var input2 = new Dictionary<string, string>
		{
			{ "Program.cs", "using System;class Program{public static void Main(){Console.WriteLine(Helper.GetMessage());}}" },
			{ "Helper.cs", "using System;public class Helper{public static string GetMessage(){return 4;}}" }
		};

		var result2 = await SimpleCompiler.CodeRunner(input2);

		Assert.NotNull(result);
		Assert.Contains("error CS0103", result.Values.First());

		Assert.NotNull(result2);
		Assert.Contains("error CS0029", result2.Values.First());
	}
}
