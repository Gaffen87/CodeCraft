using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;
/// <summary>
/// Data class for saving code submissions.
/// </summary>
internal sealed class Data
{
	/// <summary>
	/// Saves a code submission to the database.
	/// </summary>
	/// <param name="dbContext"> The application database context.</param>
	/// <param name="code"> The code submission to save.</param>
	/// <param name="group"> The group associated with the submission.</param>
	/// <param name="step"> The exercise step associated with the submission.</param>
	/// <returns></returns>
	public static async Task<CodeSubmission> SaveCodeSubmission(IAppDbContext dbContext, CodeSubmission code, Group group, ExerciseStep step)
	{
		code.SubmittedBy = group;
		code.ExerciseStep = step;

		await dbContext.Submissions.AddAsync(code);
		await dbContext.SaveChangesAsync();

		return code;
	}
	/// <summary>
	/// Retrieves a list of code submissions for a given group ID.
	/// </summary>
	/// <param name="dbContext"> The application database context.</param>
	/// <param name="id"> The ID of the group to retrieve submissions for.</param>
	/// <returns>> A list of code submissions associated with the specified group ID.</returns>
	public static async Task<Group> GetGroup(IAppDbContext dbContext, Guid id)
	{
		return await dbContext.Groups.Include(x => x.Members).SingleOrDefaultAsync(g => g.Id == id);
	}
	/// <summary>
	/// Retrieves an exercise step by its ID.
	/// </summary>
	/// <param name="dbContext"> The application database context.</param>
	/// <param name="id"> The ID of the exercise step to retrieve.</param>
	/// <returns>> The exercise step associated with the specified ID.</returns>
	public static async Task<ExerciseStep> GetExerciseStep(IAppDbContext dbContext, Guid id)
	{
		return await dbContext.ExerciseStep.SingleOrDefaultAsync(g => g.Id == id);
	}

	public static async Task SetStepAsCompleted(IAppDbContext context, ExerciseStep step, List<User> users)
	{
		foreach (var user in users)
		{
			var userProgress = await context.StepProgress.Include(x => x.ExerciseStep).Where(x => x.User.Id == user.Id).ToListAsync();

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

		}

		await context.SaveChangesAsync();
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
