using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;

public class SessionRequestEventHandler(IHubContext<AppHub> hub) : IEventHandler<SessionRequestEvent>
{
	public async Task HandleAsync(SessionRequestEvent eventModel, CancellationToken ct)
	{

	}
}
