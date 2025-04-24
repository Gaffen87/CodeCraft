using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Reflection;

namespace SignalR.PepR;

public abstract class HubMethodHandler<TPayload> : IHubMethodHandler
{
	public virtual string MethodName
	{
		get
		{
			var type = GetType();

			var attr = type.GetCustomAttribute<MethodNameAttribute>();
			if (attr != null)
			{
				return attr.Name;
			}

			return type.Name.Replace("Handler", "");
		}
	}

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
