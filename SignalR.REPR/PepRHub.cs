using Microsoft.AspNetCore.SignalR;

namespace SignalR.PepR;

public class PepRHub : Hub
{
	private readonly IEnumerable<IHubMethodHandler> _methodHandlers;

	public PepRHub(IEnumerable<IHubMethodHandler> methodHandlers)
	{
		_methodHandlers = methodHandlers;
	}

	public async Task InvokeMethod(string methodName, object payload)
	{
		var handler = _methodHandlers.FirstOrDefault(h => h.MethodName == methodName)
			?? throw new HubException($"No handler found for {methodName}");

		await handler.HandleAsync(Context, payload);
	}
}
