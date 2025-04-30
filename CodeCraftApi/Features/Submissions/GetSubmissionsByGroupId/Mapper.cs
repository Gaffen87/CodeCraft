using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;

internal sealed class Mapper : Mapper<Request, Response, List<CodeSubmission>>
{
	public override Response FromEntity(List<CodeSubmission> e)
		=> new()
		{
			SubmissionResults = e.ConvertAll(x => x.Result)
		};
}