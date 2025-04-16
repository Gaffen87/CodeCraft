using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.Groups.AddToGroup;

internal sealed class Data
{
	public async static Task<List<User>> AddUserToGroup(AppDbContext context, string groupName, string userId)
	{
		if (await GroupExists(context, groupName) == false)
		{
			await CreateNewGroup(context, groupName);
		}

		var group = await context.Groups
			.Include(g => g.Members)
			.SingleOrDefaultAsync(g => g.Name == groupName);

		var user = await context.Users
			.Include(x => x.Groups)
			.SingleOrDefaultAsync(x => x.Id == Guid.Parse(userId));

		if (user != null && group != null)
		{
			group.Members.Add(user);
			user.Groups.Add(group);
		}

		await context.SaveChangesAsync();

		return group!.Members;
	}
	public async static Task<Guid> GetIdByName(AppDbContext context, string groupName)
	{
		var group = await context.Groups.FirstOrDefaultAsync(g => g.Name == groupName);
		return group!.Id;
	}

	public async static Task<bool> GroupExists(AppDbContext context, string groupName)
	{
		return await context.Groups.FirstOrDefaultAsync(g => g.Name == groupName) != null;
	}

	public async static Task CreateNewGroup(AppDbContext context, string groupName)
	{
		Group newGroup = new()
		{
			Id = Guid.NewGuid(),
			Name = groupName,
			GroupSize = GroupSize.Team,
			IsActive = true,
			CreatedAt = DateTimeOffset.UtcNow,
			UpdatedAt = DateTimeOffset.UtcNow,
			Members = [],
			Exercises = [],
		};

		await context.Groups.AddAsync(newGroup);
		context.SaveChanges();
	}
}
