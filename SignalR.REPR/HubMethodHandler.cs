using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CodeCraftApi.SignalR;

public abstract class HubMethodHandler<TPayload> : IHubMethodHandler
{
	public virtual string MethodName => GetType().Name.Replace("Handler", "");

	public async Task HandleAsync(HubCallerContext context, object payload)
	{
		TPayload? typedPayload;

		try
		{
			typedPayload = JsonConvert.DeserializeObject<TPayload>(payload.ToString() ?? "");
		}
		catch (Exception ex)
		{
			throw new HubException($"Invalid payload for {MethodName}, {ex.Message}");
		}

		if (typedPayload == null)
		{
			throw new HubException($"Serialization failed for {MethodName}");
		}

		await HandleAsync(context, typedPayload);
	}

	protected abstract Task HandleAsync(HubCallerContext context, TPayload payload);
}
