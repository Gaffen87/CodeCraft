using CodeCraftApi.Domain.DomainEvents;
using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.Groups.SignalR.AddToGroup;

internal sealed class Data
{
	public async static Task<List<User>> AddUserToGroup(IAppDbContext context, string groupName, string userId)
	{
		bool exists = await GroupExists(context, groupName);
		bool created = false;

		if (!exists)
		{
			var id = await CreateNewGroup(context, groupName);

			await new GroupCreatedEvent(id, groupName, []).PublishAsync();

			created = true;
		}

		User? user = await GetUser(context, userId);
		Group? group = await GetGroup(context, groupName);

		if (user != null && group != null)
		{
			var userGroup = context.Groups.Where(m => m.Members.Any(u => u.Id == user.Id)).SingleOrDefault();
			if (userGroup != null)
			{
				userGroup.Members.Remove(user);
				await new UserLeftGroupEvent(userGroup.Id, user.Id).PublishAsync();
			}

			if (created)
			{
				await context.SaveChangesAsync();

				return group!.Members;
			}

			group.Members.Add(user);

			await new UserJoinedGroupEvent(
				group.Id,
				groupName,
				[user]).PublishAsync();
		}

		await context.SaveChangesAsync();

		return group!.Members;
	}

	private static async Task<Group?> GetGroup(IAppDbContext context, string groupName) => await context.Groups
				.Include(g => g.Members)
				.SingleOrDefaultAsync(g => g.Name == groupName);
	private static async Task<User?> GetUser(IAppDbContext context, string userId) => await context.Users.SingleOrDefaultAsync(x => x.Id == Guid.Parse(userId));

	public async static Task<Guid> GetIdByName(IAppDbContext context, string groupName)
	{
		var group = await context.Groups.FirstOrDefaultAsync(g => g.Name == groupName);
		return group!.Id;
	}

	public async static Task<bool> GroupExists(IAppDbContext context, string groupName)
	{
		return await context.Groups.FirstOrDefaultAsync(g => g.Name == groupName) != null;
	}

	public async static Task<Guid> CreateNewGroup(IAppDbContext context, string groupName)
	{
		Group newGroup = new()
		{
			Id = Guid.NewGuid(),
			Name = groupName,
			IsActive = true,
			CreatedAt = DateTimeOffset.UtcNow,
			UpdatedAt = DateTimeOffset.UtcNow,
			Members = [],
			Exercises = [],
		};

		await context.Groups.AddAsync(newGroup);
		await context.SaveChangesAsync();

		return newGroup.Id;
	}
}
