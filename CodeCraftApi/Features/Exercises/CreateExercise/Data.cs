using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.CreateExercise;

internal sealed class Data
{
	public async static Task<Exercise> CreateExerciseAsync(IAppDbContext context, Exercise exercise)
	{
		await context.Exercises.AddAsync(exercise);
		await context.SaveChangesAsync();

		return exercise;
	}
}
