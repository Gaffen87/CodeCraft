using CodeCraftApi.Features.Groups.Hubs.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace CodeCraftApi.Features.Groups.Hubs;

public interface IGroupHub
{
	Task ReceiveMessage(string message);
}

public class GroupHub : Hub<IGroupHub>
{
	public async Task AddToGroup(string groupName)
	{
		HubRequestToken userClaims = DeserializeToken();

		await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

		await Clients.Group(groupName).ReceiveMessage($"{userClaims.UserName} added to group {groupName}");
	}

	public async Task RemoveFromGroup(string groupName)
	{
		HubRequestToken userClaims = DeserializeToken();

		await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

		await Clients.Group(groupName).ReceiveMessage($"{userClaims.UserName} removed from group {groupName}");

		await Clients.User(Context.UserIdentifier).ReceiveMessage($"You have been removed from group {groupName}");
	}

	private HubRequestToken DeserializeToken()
	{
		var claims = Context.User!.FindFirst("user_metadata")!.Value;
		var metadata = JsonSerializer.Deserialize<HubRequestToken>(claims);

		return metadata!;
	}
}
