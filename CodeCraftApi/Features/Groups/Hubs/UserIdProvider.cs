using CodeCraftApi.Features.Groups.Hubs.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace CodeCraftApi.Features.Groups.Hubs;

public class UserIdProvider : IUserIdProvider
{
	public virtual string? GetUserId(HubConnectionContext connection)
	{
		var claim = connection.User.FindFirst("user_metadata").Value;
		var metadata = JsonSerializer.Deserialize<HubRequestToken>(claim);

		return metadata!.Sub;
	}
}