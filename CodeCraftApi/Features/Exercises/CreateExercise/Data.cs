using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Exercises.CreateExercise;

internal sealed class Data
{
	public async static Task<Exercise> CreateExerciseAsync(AppDbContext context, Exercise exercise)
	{
		await context.Exercises.AddAsync(exercise);
		await context.SaveChangesAsync();

		return exercise;
	}
}
