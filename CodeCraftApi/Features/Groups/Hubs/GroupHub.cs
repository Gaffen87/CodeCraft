using CodeCraftApi.Database;
using CodeCraftApi.Features.Groups.Hubs.Models;
using Microsoft.AspNetCore.SignalR;

namespace CodeCraftApi.Features.Groups.Hubs;

public record HubResponse(string Type, AddToGroupResponse Content);

public interface IGroupHub
{
	Task ReceiveMessage(HubResponse response);
}

internal sealed class GroupHub(AppDbContext context) : Hub<IGroupHub>
{
	public async Task AddToGroup(string groupName)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

		if (await Data.GroupExists(context, groupName) == false)
		{
			await Data.CreateNewGroup(context, groupName);
		}

		var members = await Data.AddUserToGroup(context, groupName, Context.UserIdentifier!);

		AddToGroupResponse content = new()
		{
			GroupName = groupName,
			Members = members
		};

		await Clients.All.ReceiveMessage(new HubResponse("group", content));
	}

	public async Task RemoveFromGroup(string groupName)
	{
		HubRequestToken userClaims = DeserializeToken();

		await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
	}

	private HubRequestToken DeserializeToken()
	{
		var claims = Context.User!.FindFirst("user_metadata")!.Value;
		var metadata = System.Text.Json.JsonSerializer.Deserialize<HubRequestToken>(claims);

		return metadata!;
	}
}
