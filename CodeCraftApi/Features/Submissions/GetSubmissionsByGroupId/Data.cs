using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;
/// <summary>
/// Data class for retrieving code submissions by group ID.
/// </summary>
internal sealed class Data
{
	/// <summary>
	/// Retrieves a list of code submissions for a given group ID.
	/// </summary>
	/// <param name="dbContext"> The application database context.</param>
	/// <param name="groupId"> The ID of the group to retrieve submissions for.</param>
	/// <returns> A list of code submissions associated with the specified group ID.</returns>
	public static async Task<List<CodeSubmission>> GetSubmissions(IAppDbContext dbContext, Guid groupId)
	{
		return await dbContext.Submissions.Include(g => g.SubmittedBy).Where(x => x.SubmittedBy.Id == groupId).ToListAsync();
	}
}
