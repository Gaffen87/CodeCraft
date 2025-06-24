using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Exercises.CompleteStep;

internal sealed class Data
{
	public static async Task SetStepAsCompleted(IAppDbContext context, Guid stepId, Guid userId)
	{
		var step = await context.ExerciseStep.FindAsync(stepId);
		var user = await context.Users.FindAsync(userId);
		var userProgress = await context.StepProgress.Where(x => x.User.Id == userId).ToListAsync();

		if (user != null && step != null)
		{
			var progress = new UserStepProgress
			{
				Id = Guid.NewGuid(),
				ExerciseStep = step,
				User = user,
				Completed = true
			};
			var exercise = await context.Exercises
				.Where(x => x.SubExercises
				.Any(x => x.Steps
				.Any(x => x.Id == step.Id)))
				.Include(x => x.SubExercises)
				.ThenInclude(y => y.Steps)
				.FirstOrDefaultAsync();

			context.StepProgress.Add(progress);
			user.StepProgress.Add(progress);
			userProgress.Add(progress);

			if (IsExerciseCompleted(exercise!, userProgress))
			{
				var exerciseProgress = new UserExerciseProgress
				{
					Id = Guid.NewGuid(),
					Exercise = exercise!,
					User = user,
					Completed = true
				};
				context.ExerciseProgress.Add(exerciseProgress);
				user.ExerciseProgress.Add(exerciseProgress);
			}

			await context.SaveChangesAsync();
		}
	}

	private static bool IsExerciseCompleted(Exercise exercise, List<UserStepProgress> userStepProgress)
	{
		var steps = exercise.SubExercises
			.SelectMany(subExercise => subExercise.Steps)
			.ToList();

		var completedSteps = userStepProgress
			.Where(progress => progress.Completed)
			.Select(progress => progress.ExerciseStep.Id)
			.ToHashSet();

		return steps.All(step => completedSteps.Contains(step.Id));
	}
}
