using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Sessions.SignalR.CodeChange;
/// <summary>
/// Handler for sending code changes to all clients in a group except the sender.
/// </summary>
/// <param name="hub">The SignalR hub context.</param>
internal sealed class EditorChangedHandler(IHubContext<AppHub> hub) : HubMethodHandler<CodeChangePayload>
{
	protected override async Task HandleAsync(HubCallerContext context, CodeChangePayload payload)
	{
		await hub.Clients.GroupExcept(payload.GroupName, [context.ConnectionId]).SendCoreAsync("ReceiveEditorMessage", [payload.Changes]);
	}
}
