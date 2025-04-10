using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Exercises.UpdateExercise;

internal sealed class Data
{
	public async static Task<bool> ExerciseExists(AppDbContext context, Guid Id)
	{
		var exercise = await context.Exercises.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

		return exercise != null;
	}

	public static async Task<bool> UpdateExercise(AppDbContext context, Exercise exercise)
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

	private static void UpdateSubExerciseList(AppDbContext context, Exercise exercise, Exercise? existing)
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
