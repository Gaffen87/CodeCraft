using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;

public class CodeSubmittedHandler(IHubContext<AppHub> hubContext) : IEventHandler<CodeSubmittedEvent>
{
	public async Task HandleAsync(CodeSubmittedEvent eventModel, CancellationToken ct)
	{
		await hubContext.Clients.All.SendCoreAsync("ReceiveCodeMessage", [eventModel]);

		await hubContext.Clients.Group(eventModel.GroupName).SendCoreAsync("ReceiveConsoleMessage", [eventModel.CodeResult]);
	}
}
