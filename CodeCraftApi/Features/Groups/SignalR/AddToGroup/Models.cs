using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Groups.SignalR.AddToGroup;
/// <summary>
/// Payload class for adding a user to a group.
/// </summary>
internal sealed class AddToGroupPayload
{
	public string GroupName { get; set; }
}
/// <summary>
/// Response class for adding a user to a group.
/// </summary>
internal sealed class AddToGroupResponse
{
	public string GroupName { get; set; }
	public List<User> Members { get; set; }
}
