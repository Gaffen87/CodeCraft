using CodeCraftApi.Database;
using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.Features.Groups.RemoveFromGroup;

internal sealed class RemoveFromGroupHandler(IHubContext<AppHub> hub, AppDbContext dbContext) : HubMethodHandler<RemoveFromGroupPayload>
{
	protected override async Task HandleAsync(HubCallerContext context, RemoveFromGroupPayload payload)
	{
		await hub.Groups.RemoveFromGroupAsync(context.ConnectionId, payload.GroupName);

		var members = await Data.RemoveUserFromGroup(dbContext, payload.GroupName, context.UserIdentifier!);

		var response = new HubResponse<RemoveFromGroupResponse>
		{
			Type = HubResponseType.Group,
			Content = new RemoveFromGroupResponse
			{
				GroupName = payload.GroupName,
				Members = members
			}
		};

		await hub.Clients.All.SendCoreAsync("ReceiveMessage", [response]);
	}
}
