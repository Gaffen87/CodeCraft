using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.CreateExercise;
/// <summary>
/// Data class for creating exercises.
/// </summary>
internal sealed class Data
{
	/// <summary>
	/// Creates a new exercise in the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="exercise"> The exercise to create.</param>
	/// <returns> The created exercise.</returns>
	public static async Task<Exercise> CreateExerciseAsync(IAppDbContext context, Exercise exercise)
	{
		await context.Exercises.AddAsync(exercise);
		await context.SaveChangesAsync();

		return exercise;
	}
}
