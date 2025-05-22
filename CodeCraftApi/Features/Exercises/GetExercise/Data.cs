using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Exercises.GetExercise;
/// <summary>
/// Data class for retrieving exercises.
/// </summary>
internal sealed class Data
{
	/// <summary>
	/// Retrieves an exercise by its ID from the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="id"> The ID of the exercise to retrieve.</param>
	/// <returns> The exercise with the specified ID, or null if not found.</returns>
	public static async Task<Exercise?> GetExerciseAsync(IAppDbContext context, Guid id)
	{
		return await context.Exercises.Include(x => x.SubExercises).ThenInclude(x => x.Steps).Include(x => x.Categories).Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == id);
	}
}
