using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;
/// <summary>
/// Event handler for the CodeSubmittedEvent.
/// </summary>
/// <param name="hub"> The SignalR hub context.</param>
public class UserJoinedGroupHandler(IHubContext<AppHub> hub) : IEventHandler<UserJoinedGroupEvent>
{
	/// <summary>
	/// Handles the CodeSubmittedEvent by sending messages to all clients and a specific group.
	/// </summary>
	public Task HandleAsync(UserJoinedGroupEvent eventModel, CancellationToken ct)
		=> hub.Clients.All.SendCoreAsync("ReceiveGroupMessage", [eventModel]);
}
