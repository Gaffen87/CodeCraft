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
		return await dbContext.Groups.SingleOrDefaultAsync(g => g.Id == id);
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
}
