namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using CodeCraftApi.Features.DbAbstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
/// <summary>
/// Data class for retrieving exercise items.
/// </summary>
internal sealed class Data
{
	public static async Task<ExerciseItem?> GetExerciseItemsAsync(IAppDbContext db, Guid id)
	{
		return await db.ExerciseItem
			.FirstOrDefaultAsync(x => x.Id == id);
	}

}