namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class CodeSubmissionRequest
{
	public List<CodeFileRequest> Files { get; set; }
	public Guid SubmittedBy { get; set; }
	public Guid ExerciseStep { get; set; }

	internal sealed class Validator : Validator<CodeSubmissionRequest>
	{
		public Validator()
		{
			//TODO validation logic
		}
	}
}

public class CodeFileRequest
{
	public string FileName { get; set; }
	public string Content { get; set; }

	internal sealed class Validator : Validator<CodeFileRequest>
	{
		public Validator()
		{
			//TODO validation logic
		}
	}
}

internal sealed class CodeSubmissionResponse
{
	public string Result { get; set; }
}
