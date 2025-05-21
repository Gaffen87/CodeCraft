namespace CodeCraftApi.Features.Groups.GetAllGroups;

using CodeCraftApi.Features.DbAbstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class Data
{
	public static async Task<List<Group>> GetGroupsAsync(IAppDbContext db)
	{
		return await db.Groups.Include(m => m.Members)
			.ToListAsync();
	}
}