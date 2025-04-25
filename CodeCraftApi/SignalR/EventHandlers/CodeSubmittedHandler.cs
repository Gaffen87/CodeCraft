using CodeCraftApi.Domain.DomainEvents;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR.EventHandlers;

public class CodeSubmittedHandler(IHubContext<AppHub> hubContext, ILogger<CodeSubmittedHandler> logger) : IEventHandler<CodeSubmittedEvent>
{
	public async Task HandleAsync(CodeSubmittedEvent eventModel, CancellationToken ct)
	{
		logger.LogInformation("Code Submitted | Result: {CodeResult}", eventModel.CodeResult);

		await hubContext.Clients.All.SendCoreAsync("ReceiveMessage", [eventModel.CodeResult]);
	}
}
