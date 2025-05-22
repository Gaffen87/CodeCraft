namespace CodeCraftApi.Features.Groups.GetAllGroups;

using CodeCraftApi.Features.DbAbstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
/// <summary>
/// Data class for retrieving groups.
/// </summary>
internal sealed class Data
{
	/// <summary>
	/// Retrieves all groups from the database asynchronously.
	/// </summary>
	/// <param name="db"> The application database context.</param>
	/// <returns>> A list of groups.</returns>
	public static async Task<List<Group>> GetGroupsAsync(IAppDbContext db)
	{
		return await db.Groups.Include(m => m.Members)
			.ToListAsync();
	}
}