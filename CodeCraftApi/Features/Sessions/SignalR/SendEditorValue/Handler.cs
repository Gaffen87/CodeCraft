using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Sessions.SignalR.SendEditorValue;

internal sealed class SendEditorValueHandler(IHubContext<AppHub> hub) : HubMethodHandler<SendEditorValuePayload>
{
	protected override async Task HandleAsync(HubCallerContext context, SendEditorValuePayload payload)
	{
		await hub.Clients.Users(payload.UserId.ToString()).SendCoreAsync("ReceiveEditorValueMessage", [payload.EditorValue]);
	}
}
