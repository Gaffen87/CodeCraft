using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.ToggleVisibility;

internal sealed class Data
{
	public async static Task ToggleVisibility(IAppDbContext context, Request request)
	{
		foreach (var change in request.Changes)
		{
			var exercise = await context.Exercises.FindAsync(change.ExerciseId);

			if (exercise != null) { exercise.IsVisible = change.IsVisible; }

			await context.SaveChangesAsync();
		}
	}
}
