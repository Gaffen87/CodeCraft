using Compiler;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Request
{
	public CodeExecutionRequest CodeRequest { get; set; }

	//TODO complete request model
	//public Guid SubmittedBy { get; set; }
	//public Guid ExerciseStep { get; set; }

	internal sealed class Validator : Validator<Request>
	{
		public Validator()
		{
			//TODO validation logic
		}
	}
}

internal sealed class Response
{
	public string Result { get; set; }
}
