using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;
/// <summary>
/// Event handler for the CodeSubmittedEvent.
/// </summary>
/// <param name="hubContext"> The SignalR hub context.</param>
public class CodeSubmittedHandler(IHubContext<AppHub> hubContext) : IEventHandler<CodeSubmittedEvent>
{
	/// <summary>
	/// Handles the CodeSubmittedEvent by sending messages to all clients and a specific group.
	/// </summary>
	/// <param name="eventModel"> The event model containing the code submission details.</param>
	/// <param name="ct"> The cancellation token.</param>
	public async Task HandleAsync(CodeSubmittedEvent eventModel, CancellationToken ct)
	{
		await hubContext.Clients.All.SendCoreAsync("ReceiveCodeMessage", [eventModel]);

		await hubContext.Clients.Group(eventModel.GroupName).SendCoreAsync("ReceiveConsoleMessage", [eventModel.CodeResult]);
	}
}
