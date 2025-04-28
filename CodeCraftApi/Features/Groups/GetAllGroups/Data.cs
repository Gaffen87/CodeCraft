namespace CodeCraftApi.Features.Groups.GetAllGroups;

using Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class Data
{
	public static async Task<List<Group>> GetGroupsAsync(AppDbContext db)
	{
		return await db.Groups.Include(m => m.Members)
			.ToListAsync();
	}
}