using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Data
{
	public async static Task SaveCodeSubmission(AppDbContext dbContext, CodeSubmission code, Group group, ExerciseStep step)
	{
		code.SubmittedBy = group;
		code.ExerciseStep = step;

		await dbContext.Submissions.AddAsync(code);
		await dbContext.SaveChangesAsync();
	}

	public async static Task<Group> GetGroup(AppDbContext dbContext, Guid id)
	{
		return await dbContext.Groups.SingleOrDefaultAsync(g => g.Id == id);
	}

	public async static Task<ExerciseStep> GetExerciseStep(AppDbContext dbContext, Guid id)
	{
		return await dbContext.ExerciseStep.SingleOrDefaultAsync(g => g.Id == id);
	}
}
