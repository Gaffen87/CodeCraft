using FluentValidation;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;

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

internal sealed class Response
{
	public List<string> SubmissionResults { get; set; }
}