using CodeCraftApi.Domain.DomainEvents;
using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.Groups.SignalR.AddToGroup;
/// <summary>
/// Data class for managing group membership.
/// </summary>
internal sealed class Data
{
	/// <summary>
	/// Adds a user to a group. If the group does not exist, it creates a new group.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="groupName"> The name of the group to add the user to.</param>
	/// <param name="userId"> The ID of the user to add to the group.</param>
	/// <returns> A list of members in the group after the user has been added.</returns>
	public static async Task<List<User>> AddUserToGroup(IAppDbContext context, string groupName, string userId)
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
	/// <summary>
	/// Retrieves a group by its name from the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="groupName"> The name of the group to retrieve.</param>
	/// <returns>> The group with the specified name, or null if not found.</returns>
	private static async Task<Group?> GetGroup(IAppDbContext context, string groupName) => await context.Groups
				.Include(g => g.Members)
				.SingleOrDefaultAsync(g => g.Name == groupName);
	/// <summary>
	/// Retrieves a user by their ID from the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="userId"> The ID of the user to retrieve.</param>
	/// <returns> The user with the specified ID, or null if not found.</returns>
	private static async Task<User?> GetUser(IAppDbContext context, string userId) => await context.Users.SingleOrDefaultAsync(x => x.Id == Guid.Parse(userId));
	/// <summary>
	/// Retrieves the ID of a group by its name from the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="groupName"> The name of the group to retrieve the ID for.</param>
	/// <returns>> The ID of the group with the specified name.</returns>
	public static async Task<Guid> GetIdByName(IAppDbContext context, string groupName)
	{
		var group = await context.Groups.FirstOrDefaultAsync(g => g.Name == groupName);
		return group!.Id;
	}
	/// <summary>
	/// Checks if a group exists in the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="groupName"> The name of the group to check.</param>
	/// <returns>> True if the group exists, false otherwise.</returns>
	public async static Task<bool> GroupExists(IAppDbContext context, string groupName)
	{
		return await context.Groups.FirstOrDefaultAsync(g => g.Name == groupName) != null;
	}
/// <summary>
///  Creates a new group in the database asynchronously.
/// </summary>
/// <param name="context"> The  database context.</param>
/// <param name="groupName"> The name of the group to create.</param>
/// <returns>> The ID of the created group.</returns>
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
