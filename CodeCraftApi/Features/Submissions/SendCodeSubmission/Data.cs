using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Data
{
	public async static Task SaveCodeSubmission(AppDbContext dbContext, CodeSubmission code, Guid groupId, Guid stepId)
	{
		code.SubmittedBy = await GetGroup(dbContext, groupId);
		code.ExerciseStep = await GetExerciseStep(dbContext, stepId);

		await dbContext.Submissions.AddAsync(code);
		await dbContext.SaveChangesAsync();
	}

	private async static Task<Group> GetGroup(AppDbContext dbContext, Guid id)
	{
		return await dbContext.Groups.SingleOrDefaultAsync(g => g.Id == id);
	}

	public async static Task<ExerciseStep> GetExerciseStep(AppDbContext dbContext, Guid id)
	{
		return await dbContext.ExerciseStep.SingleOrDefaultAsync(g => g.Id == id);
	}
}
