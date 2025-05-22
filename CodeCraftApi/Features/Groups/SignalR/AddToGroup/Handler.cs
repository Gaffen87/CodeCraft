using CodeCraftApi.Features.DbAbstraction;
using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Groups.SignalR.AddToGroup;
/// <summary>
/// Handler for adding a user to a group.
/// </summary>
/// <param name="hub"> The SignalR hub context.</param>
/// <param name="dbContext"> The application database context.</param>
internal sealed class AddToGroupHandler(IHubContext<AppHub> hub, IAppDbContext dbContext) : HubMethodHandler<AddToGroupPayload>
{
	/// <summary>
	/// Handles the addition of a user to a group.
	/// </summary>
	/// <param name="callerContext"> The context of the caller.</param>
	/// <param name="payload"> The payload containing the group name.</param>
	protected override async Task HandleAsync(HubCallerContext callerContext, AddToGroupPayload payload)
	{
		await hub.Groups.AddToGroupAsync(callerContext.ConnectionId, payload.GroupName);

		var members = await Data.AddUserToGroup(dbContext, payload.GroupName, callerContext.UserIdentifier!);
	}
}
