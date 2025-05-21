using CodeCraftApi.Features.DbAbstraction;
using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Groups.SignalR.AddToGroup;

internal sealed class AddToGroupHandler(IHubContext<AppHub> hub, IAppDbContext dbContext) : HubMethodHandler<AddToGroupPayload>
{
	protected override async Task HandleAsync(HubCallerContext callerContext, AddToGroupPayload payload)
	{
		await hub.Groups.AddToGroupAsync(callerContext.ConnectionId, payload.GroupName);

		var members = await Data.AddUserToGroup(dbContext, payload.GroupName, callerContext.UserIdentifier!);

		//var response = new HubResponse<AddToGroupResponse>
		//{
		//	Type = HubResponseType.Group,
		//	Content = new AddToGroupResponse
		//	{
		//		GroupName = payload.GroupName,
		//		Members = members
		//	}
		//};

		//await hub.Clients.All.SendCoreAsync("ReceiveMessage", [response]);
	}
}
