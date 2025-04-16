using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR;

public class ReprHub : Hub
{
	private readonly IEnumerable<IHubMethodHandler> _methodHandlers;

	public ReprHub(IEnumerable<IHubMethodHandler> methodHandlers)
	{
		_methodHandlers = methodHandlers ?? [];
	}

	public async Task InvokeMethod(string methodName, object payload)
	{
		var handler = _methodHandlers.FirstOrDefault(h => h.MethodName == methodName) ?? throw new HubException($"No handler found for {methodName}");

		await handler.HandleAsync(Context, payload);
	}
}
