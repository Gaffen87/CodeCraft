using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;

public class GroupCreatedHandler(IHubContext<AppHub> hubContext, ILogger<GroupCreatedHandler> logger) : IEventHandler<GroupCreatedEvent>
{
	public async Task HandleAsync(GroupCreatedEvent eventModel, CancellationToken ct)
	{
		logger.LogInformation("Group created | Name: {GroupName}", eventModel.GroupName);

		await hubContext.Clients.All.SendCoreAsync("ReceiveMessage", [eventModel.GroupName]);
	}
}
