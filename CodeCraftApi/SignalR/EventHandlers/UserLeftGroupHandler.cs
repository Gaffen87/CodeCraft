using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;
/// <summary>
/// Event handler for the CodeSubmittedEvent.
/// </summary>
/// <param name="hub"> The SignalR hub context.</param>
public class UserLeftGroupHandler(IHubContext<AppHub> hub) : IEventHandler<UserLeftGroupEvent>
{
	/// <summary>
	/// Handles the CodeSubmittedEvent by sending messages to all clients and a specific group.
	/// </summary>
	/// <param name="eventModel"> The event model containing the code submission details.</param>
	/// <param name="ct"> The cancellation token.</param>
	/// <returns> A task representing the asynchronous operation.</returns>
	public Task HandleAsync(UserLeftGroupEvent eventModel, CancellationToken ct) =>
		hub.Clients.All.SendCoreAsync("ReceiveGroupMessage", [eventModel]);
}
