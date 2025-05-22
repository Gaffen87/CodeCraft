using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Exercises.UpdateExercise;

/// <summary>
/// Data class for updating exercises.
/// </summary>
internal sealed class Data
{
	/// <summary>
	/// Checks if an exercise exists in the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="Id"> The ID of the exercise to check.</param>
	/// <returns>> True if the exercise exists, false otherwise.</returns>
	public async static Task<bool> ExerciseExists(IAppDbContext context, Guid Id)
	{
		var exercise = await context.Exercises.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

		return exercise != null;
	}
	/// <summary>
	/// Updates an exercise in the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="exercise"> The exercise to update.</param>
	/// <returns>> True if the update was successful, false otherwise.</returns>
	public static async Task<bool> UpdateExercise(IAppDbContext context, Exercise exercise)
	{
		var existing = await context.Exercises
		.Include(e => e.SubExercises)
			.ThenInclude(se => se.Steps)
			.Include(x => x.Groups)
			.Include(x => x.Categories)
		.FirstOrDefaultAsync(e => e.Id == exercise.Id);

		if (existing == null) return false;

		existing.Title = exercise.Title;
		existing.Summary = exercise.Summary;
		existing.Author = exercise.Author;
		existing.ExerciseDifficulty = exercise.ExerciseDifficulty;
		existing.CreatedAt = exercise.CreatedAt;
		existing.IsDeleted = exercise.IsDeleted;
		existing.UpdatedAt = DateTimeOffset.UtcNow;

		UpdateSubExerciseList(context, exercise, existing);

		return await context.SaveChangesAsync() > 0;
	}
	/// <summary>
	/// Updates the list of sub-exercises for an existing exercise.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="exercise"> The exercise containing the new sub-exercises.</param>
	/// <param name="existing"> The existing exercise to update.</param>
	private static void UpdateSubExerciseList(IAppDbContext context, Exercise exercise, Exercise? existing)
	{
		context.ExerciseItem.RemoveRange(existing!.SubExercises);
		foreach (var item in existing.SubExercises)
		{
			context.ExerciseStep.RemoveRange(item.Steps);
		}

		existing.SubExercises = exercise.SubExercises;
		context.ExerciseItem.AddRange(existing.SubExercises);
	}
}
