using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Exercises.GetExercise;

internal sealed class Data
{
	public static async Task<Exercise?> GetExerciseAsync(AppDbContext context, Guid id)
	{
		return await context.Exercises.Include(x => x.SubExercises).ThenInclude(x => x.Steps).Include(x => x.Categories).Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == id);
	}
}
