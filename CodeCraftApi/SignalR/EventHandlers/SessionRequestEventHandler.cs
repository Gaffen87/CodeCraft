using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;
/// <summary>
/// Event handler for the CodeSubmittedEvent.
/// </summary>
/// <param name="hub"> The SignalR hub context.</param>
public class SessionRequestEventHandler(IHubContext<AppHub> hub) : IEventHandler<SessionRequestEvent>
{
	/// <summary>
	/// Handles the CodeSubmittedEvent by sending messages to all clients and a specific group.
	/// </summary>
	public async Task HandleAsync(SessionRequestEvent eventModel, CancellationToken ct)
	{

	}
}
