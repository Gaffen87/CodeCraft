using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;
/// <summary>
/// Mapper class for converting a list of code submissions to a response object.
/// </summary>
internal sealed class Mapper : Mapper<Request, Response, List<CodeSubmission>>
{
	/// <summary>
	/// Converts a Request object to a list of CodeSubmission entities.
	/// </summary>
	/// <param name="e"> The Request object to convert.</param>
	/// <returns> The converted list of CodeSubmission entities.</returns>
	public override Response FromEntity(List<CodeSubmission> e)
		=> new()
		{
			Submissions = e.ConvertAll(submission => new Submission()
			{
				Content = submission.Result,
				GroupName = submission.SubmittedBy.Name,
				TimeStamp = submission.SubmitDate,
				IsSuccess = submission.IsSuccess
			})
		};
}