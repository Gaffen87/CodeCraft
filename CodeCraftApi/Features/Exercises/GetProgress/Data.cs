using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Exercises.GetProgress;

internal sealed class Data
{
	public static async Task<Response> GetUserProgress(IAppDbContext context, Guid userId)
	{
		var user = await context.Users.Include(x => x.ExerciseProgress).ThenInclude(x => x.Exercise).Include(x => x.StepProgress).ThenInclude(x => x.ExerciseStep).FirstOrDefaultAsync(x => x.Id == userId);

		return new Response
		{
			ExerciseProgress = user!.ExerciseProgress,
			StepProgress = user.StepProgress,
		};
	}
}
