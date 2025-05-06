using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;

internal sealed class Mapper : Mapper<Request, Response, List<CodeSubmission>>
{
	public override Response FromEntity(List<CodeSubmission> e)
		=> new()
		{
			Submissions = e.ConvertAll(submission => new Submission()
			{
				Content = submission.Result,
				GroupId = submission.SubmittedBy.Id,
				GroupName = submission.SubmittedBy.Name,
			})
		};
}