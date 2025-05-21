using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Data
{
	public async static Task<CodeSubmission> SaveCodeSubmission(IAppDbContext dbContext, CodeSubmission code, Group group, ExerciseStep step)
	{
		code.SubmittedBy = group;
		code.ExerciseStep = step;

		await dbContext.Submissions.AddAsync(code);
		await dbContext.SaveChangesAsync();

		return code;
	}

	public async static Task<Group> GetGroup(IAppDbContext dbContext, Guid id)
	{
		return await dbContext.Groups.SingleOrDefaultAsync(g => g.Id == id);
	}

	public async static Task<ExerciseStep> GetExerciseStep(IAppDbContext dbContext, Guid id)
	{
		return await dbContext.ExerciseStep.SingleOrDefaultAsync(g => g.Id == id);
	}
}
