using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;

public class UserLeftGroupHandler(IHubContext<AppHub> hub) : IEventHandler<UserLeftGroupEvent>
{
	public Task HandleAsync(UserLeftGroupEvent eventModel, CancellationToken ct) =>
		hub.Clients.All.SendCoreAsync("ReceiveGroupMessage", [eventModel]);
}
