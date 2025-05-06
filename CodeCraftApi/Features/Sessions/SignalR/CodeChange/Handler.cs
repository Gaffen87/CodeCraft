using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Sessions.SignalR.CodeChange;

internal sealed class EditorChangedHandler(IHubContext<AppHub> hub, ILogger<EditorChangedHandler> logger) : HubMethodHandler<CodeChangePayload>
{
	protected override async Task HandleAsync(HubCallerContext context, CodeChangePayload payload)
	{
		await hub.Clients.GroupExcept(payload.GroupName, [context.ConnectionId]).SendCoreAsync("ReceiveEditorMessage", [payload.Changes]);
	}
}
