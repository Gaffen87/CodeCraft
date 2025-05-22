using FluentValidation;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;
/// <summary>
/// Request class for getting submissions by group ID.
/// </summary>
internal sealed class Request
{
	public Guid GroupId { get; set; }

	internal sealed class Validator : Validator<Request>
	{
		public Validator()
		{
			RuleFor(x => x.GroupId).NotEmpty();
		}
	}
}
/// <summary>
/// Response class for getting submissions by group ID.
/// </summary>
internal sealed class Response
{
	public List<Submission> Submissions { get; set; }
}
/// <summary>
/// Submission class representing a code submission.
/// </summary>
internal sealed class Submission
{
	public string Content { get; set; }
	public string GroupName { get; set; }
	public DateTimeOffset TimeStamp { get; set; }
	public bool IsSuccess { get; set; }
}