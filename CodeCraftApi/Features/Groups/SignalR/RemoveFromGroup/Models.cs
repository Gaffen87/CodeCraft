namespace CodeCraftApi.Features.Groups.SignalR.RemoveFromGroup;
/// <summary>
/// Payload class for removing a user from a group.
/// </summary>
internal sealed class RemoveFromGroupPayload
{
	public string GroupName { get; set; }
}