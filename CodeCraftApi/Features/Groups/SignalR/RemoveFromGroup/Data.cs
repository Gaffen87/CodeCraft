using CodeCraftApi.Domain.DomainEvents;
using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;

namespace CodeCraftApi.Features.Groups.SignalR.RemoveFromGroup;
/// <summary>
/// Data class for removing a user from a group.
/// </summary>
internal sealed class Data
{
	/// <summary>
	/// Removes a user from a group in the database asynchronously.
	/// </summary>
	/// <param name="context"> The application database context.</param>
	/// <param name="groupName"> The name of the group to remove the user from.</param>
	/// <param name="userId"> The ID of the user to remove from the group.</param>
	/// <returns>> A list of members in the group after the user has been removed.</returns>
	public static async Task<List<User>> RemoveUserFromGroup(IAppDbContext context, string groupName, string userId)
	{
		var group = await context.Groups.Include(m => m.Members).SingleOrDefaultAsync(g => g.Name == groupName);

		var user = await context.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

		group.Members.Remove(user);

		await new UserLeftGroupEvent(group.Id, user.Id).PublishAsync();

		await context.SaveChangesAsync();

		return group.Members;
	}
}
