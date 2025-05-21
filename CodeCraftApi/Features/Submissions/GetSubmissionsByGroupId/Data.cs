using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;

internal sealed class Data
{
	public async static Task<List<CodeSubmission>> GetSubmissions(IAppDbContext dbContext, Guid groupId)
	{
		return await dbContext.Submissions.Include(g => g.SubmittedBy).Where(x => x.SubmittedBy.Id == groupId).ToListAsync();
	}
}
