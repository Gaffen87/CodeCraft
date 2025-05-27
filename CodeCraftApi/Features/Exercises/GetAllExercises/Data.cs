using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Exercises.GetAllExercises;

internal sealed class Data
{
	public async static Task<List<Exercise>> GetExercises(IAppDbContext context)
	{
		return await context.Exercises.ToListAsync();
	}
}
