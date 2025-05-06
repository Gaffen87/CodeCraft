using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;

public class GroupCreatedHandler(IHubContext<AppHub> hubContext, ILogger<GroupCreatedHandler> logger) : IEventHandler<GroupCreatedEvent>
{
	public async Task HandleAsync(GroupCreatedEvent eventModel, CancellationToken ct)
	{
		await hubContext.Clients.All.SendCoreAsync("ReceiveGroupMessage", [eventModel]);
	}
}
