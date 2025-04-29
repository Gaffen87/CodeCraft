using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;

public class UserJoinedGroupHandler(IHubContext<AppHub> hub) : IEventHandler<UserJoinedGroupEvent>
{
	public Task HandleAsync(UserJoinedGroupEvent eventModel, CancellationToken ct)
		=> hub.Clients.All.SendCoreAsync("ReceiveGroupMessage", [eventModel]);
}
