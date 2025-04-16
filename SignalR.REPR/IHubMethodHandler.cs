using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.SignalR;

public interface IHubMethodHandler
{
	string MethodName { get; }
	Task HandleAsync(HubCallerContext context, object payload);
}
