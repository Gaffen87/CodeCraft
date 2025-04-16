using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Groups.RemoveFromGroup;

public class Data
{
	public async static Task<List<User>> RemoveUserFromGroup(AppDbContext context, string groupName, string userId)
	{
		var group = await context.Groups.Include(m => m.Members).SingleOrDefaultAsync(g => g.Name == groupName);

		var user = await context.Users.Include(g => g.Groups).SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

		group.Members.Remove(user);
		user.Groups.Remove(group);

		if (group.Members.Count == 0)
		{
			context.Groups.Remove(group);
		}

		await context.SaveChangesAsync();

		return group.Members;
	}
}
