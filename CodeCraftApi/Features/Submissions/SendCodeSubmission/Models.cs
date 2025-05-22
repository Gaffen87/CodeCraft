namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;
/// <summary>
/// Request class for sending code submissions.
/// </summary>
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
/// <summary>
/// Request class for sending code files.
/// </summary>
internal sealed class CodeFileRequest
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
	public bool isSuccess { get; set; }
	public string Result { get; set; }
}
