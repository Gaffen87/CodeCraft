using CodeCraftApi.Database;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Groups.SignalR.UpdateExercise;

internal sealed class Data
{
	public static async Task<string?> GetExerciseTitle(AppDbContext context, Guid stepId)
	{
		var exercise = await context.Exercises
			.Where(x => x.SubExercises.Any(sub => sub.Steps.Any(step => step.Id == stepId)))
			.FirstOrDefaultAsync();

		return exercise?.Title ?? string.Empty;
	}

	public static async Task<int?> GetExerciseItemNumber(AppDbContext context, Guid stepId)
	{
		var exerciseItem = await context.ExerciseItem.Where(x => x.Steps.Any(x => x.Id == stepId)).FirstOrDefaultAsync();

		return exerciseItem?.Number;
	}

	public static async Task<int?> GetStepIndex(AppDbContext context, Guid stepId)
	{
		var exerciseItem = await context.ExerciseItem.Include(x => x.Steps).Where(x => x.Steps.Any(x => x.Id == stepId)).FirstOrDefaultAsync();

		if (exerciseItem != null)
		{
			exerciseItem.Steps.Reverse();
			return exerciseItem.Steps.FindIndex(x => x.Id == stepId) + 1;
		}

		return null;
	}
}
