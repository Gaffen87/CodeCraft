using CodeCraftApi.Database;
using CodeCraftApi.Domain.DomainEvents;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.Groups.SignalR.AddToGroup;

internal sealed class Data
{
	public async static Task<List<User>> AddUserToGroup(AppDbContext context, string groupName, string userId)
	{
		if (await GroupExists(context, groupName) == false)
		{
			var id = await CreateNewGroup(context, groupName);

			await new GroupCreatedEvent(id, groupName, []).PublishAsync();
		}

		User? user = await GetUser(context, userId);
		Group? group = await GetGroup(context, groupName);

		if (user != null && group != null)
		{
			group.Members.Add(user);
			user.Groups.Add(group);

			await new UserJoinedGroupEvent(group.Id, groupName, [user]).PublishAsync();
		}

		await context.SaveChangesAsync();

		return group!.Members;
	}

	private static async Task<Group?> GetGroup(AppDbContext context, string groupName) => await context.Groups
				.Include(g => g.Members)
				.SingleOrDefaultAsync(g => g.Name == groupName);
	private static async Task<User?> GetUser(AppDbContext context, string userId) => await context.Users
				.Include(x => x.Groups)
				.SingleOrDefaultAsync(x => x.Id == Guid.Parse(userId));

	public async static Task<Guid> GetIdByName(AppDbContext context, string groupName)
	{
		var group = await context.Groups.FirstOrDefaultAsync(g => g.Name == groupName);
		return group!.Id;
	}

	public async static Task<bool> GroupExists(AppDbContext context, string groupName)
	{
		return await context.Groups.FirstOrDefaultAsync(g => g.Name == groupName) != null;
	}

	public async static Task<Guid> CreateNewGroup(AppDbContext context, string groupName)
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

		return newGroup.Id;
	}
}
