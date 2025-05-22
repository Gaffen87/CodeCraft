using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;
/// <summary>
/// Event handler for the CodeSubmittedEvent.
/// </summary>
/// <param name="hubContext"> The SignalR hub context.</param>
/// <param name="logger"> The logger for logging events.</param>
public class GroupCreatedHandler(IHubContext<AppHub> hubContext, ILogger<GroupCreatedHandler> logger) : IEventHandler<GroupCreatedEvent>
{
	/// <summary>
	/// Handles the GroupCreatedEvent by sending messages to all clients.
	/// </summary>
	/// <param name="eventModel"> The event model containing the group creation details.</param>
	/// <param name="ct"> The cancellation token.</param>
	public async Task HandleAsync(GroupCreatedEvent eventModel, CancellationToken ct)
	{
		await hubContext.Clients.All.SendCoreAsync("ReceiveGroupMessage", [eventModel]);
	}
}
