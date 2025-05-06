using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Sessions.SignalR.GetSession;

internal sealed class GetSessionHandler(IHubContext<AppHub> hub) : HubMethodHandler<GetSessionPayload>
{
	protected override async Task HandleAsync(HubCallerContext context, GetSessionPayload payload)
	{
		await hub.Clients.GroupExcept(payload.GroupName, [context.ConnectionId]).SendCoreAsync("ReceiveGetSessionMessage", [new { UserId = context.UserIdentifier, payload.GroupName }]);
	}
}
