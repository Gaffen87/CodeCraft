using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Exercises.GetExercise;

internal sealed class Data
{
	public static async Task<Exercise?> GetExerciseAsync(IAppDbContext context, Guid id)
	{
		return await context.Exercises.Include(x => x.SubExercises).ThenInclude(x => x.Steps).Include(x => x.Categories).Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == id);
	}
}
