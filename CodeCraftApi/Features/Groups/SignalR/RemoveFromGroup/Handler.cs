using CodeCraftApi.Features.DbAbstraction;
using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Groups.SignalR.RemoveFromGroup;
/// <summary>
/// Handler for removing a user from a group.
/// </summary>
/// <param name="hub"> The SignalR hub context.</param>
/// <param name="dbContext"> The application database context.</param>
internal sealed class RemoveFromGroupHandler(IHubContext<AppHub> hub, IAppDbContext dbContext) : HubMethodHandler<RemoveFromGroupPayload>
{
	protected override async Task HandleAsync(HubCallerContext context, RemoveFromGroupPayload payload)
	{
		await hub.Groups.RemoveFromGroupAsync(context.ConnectionId, payload.GroupName);

		var members = await Data.RemoveUserFromGroup(dbContext, payload.GroupName, context.UserIdentifier!);

	}
}
