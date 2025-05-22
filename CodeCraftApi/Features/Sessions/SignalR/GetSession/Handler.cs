using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Sessions.SignalR.GetSession;
/// <summary>
/// Handler for sending a message to all clients in a group except the sender.
/// </summary>
/// <param name="hub">The SignalR hub context.</param>
internal sealed class GetSessionHandler(IHubContext<AppHub> hub) : HubMethodHandler<GetSessionPayload>
{
	/// <summary>
	/// Handles the sending of a message to all clients in a group except the sender.
	/// </summary>
	/// <param name="context">The context of the caller.</param>
	/// <param name="payload">The payload containing the group name.</param>
	protected override async Task HandleAsync(HubCallerContext context, GetSessionPayload payload)
	{
		await hub.Clients.GroupExcept(payload.GroupName, [context.ConnectionId]).SendCoreAsync("ReceiveGetSessionMessage", [new { UserId = context.UserIdentifier, payload.GroupName }]);
	}
}
