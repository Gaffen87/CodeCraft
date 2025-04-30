using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;

internal sealed class Data
{
	public async static Task<List<CodeSubmission>> GetSubmissions(AppDbContext dbContext, Guid groupId)
	{
		return await dbContext.Submissions.Where(x => x.SubmittedBy.Id == groupId).ToListAsync();
	}
}
