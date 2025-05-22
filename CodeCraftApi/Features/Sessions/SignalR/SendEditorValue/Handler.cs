using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Sessions.SignalR.SendEditorValue;
/// <summary>
/// Handler for sending editor values to a specific user.
/// </summary>
/// <param name="hub">The SignalR hub context.</param>
internal sealed class SendEditorValueHandler(IHubContext<AppHub> hub) : HubMethodHandler<SendEditorValuePayload>
{
	/// <summary>
	/// Handles the sending of editor values to a specific user.
	/// </summary>
	/// <param name="context"> The context of the caller.</param>
	/// <param name="payload"> The payload containing the user ID and editor value.</param>
	protected override async Task HandleAsync(HubCallerContext context, SendEditorValuePayload payload)
	{
		await hub.Clients.Users(payload.UserId.ToString()).SendCoreAsync("ReceiveEditorValueMessage", [payload.EditorValue]);
	}
}
