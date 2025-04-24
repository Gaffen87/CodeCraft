using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Data
{
	public async static Task SaveCodeSubmission(AppDbContext dbContext, CodeSubmission code)
	{
		await dbContext.Submissions.AddAsync(code);
		await dbContext.SaveChangesAsync();
	}
}
