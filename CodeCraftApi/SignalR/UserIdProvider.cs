using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace CodeCraftApi.SignalR;

public class UserIdProvider : IUserIdProvider
{
	public virtual string? GetUserId(HubConnectionContext connection)
	{
		var claim = connection.User.FindFirst("user_metadata").Value;
		var metadata = JsonSerializer.Deserialize<AccessToken>(claim);

		return metadata!.Sub;
	}
}