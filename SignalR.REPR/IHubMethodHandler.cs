using Microsoft.AspNetCore.SignalR;

namespace SignalR.PepR;

public interface IHubMethodHandler
{
	string MethodName { get; }
	Task HandleAsync(HubCallerContext context, object payload);
}
