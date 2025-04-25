namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Summary : EndpointSummary
{
	public Summary()
	{
		Summary = "Send code for execution";
		ExampleRequest = new CodeSubmissionRequest
		{
			Files = [new()
			{
				FileName = "Program.cs",
				Content = @"using System;

class Program
{ 
	public static void Main() 
	{ 
		Console.WriteLine(Helper.GetMessage()); 
	} 
}"
			},
			new() {
				FileName = "Helper.cs",
				Content = @"using System;

public class Helper
{
	public static string GetMessage()
	{ 
		return ""Hello, World!""; 
	} 
}"
			}],
			ExerciseStep = Guid.Parse("ae487a06-a508-4191-9f22-faae7f9463ba"),
			SubmittedBy = Guid.Parse("a2e663d4-373c-4f65-b814-908a83c16e2c")
		};
	}
}
