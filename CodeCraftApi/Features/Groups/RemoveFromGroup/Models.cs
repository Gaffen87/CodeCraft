using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Groups.RemoveFromGroup;

internal sealed class RemoveFromGroupPayload
{
	public string GroupName { get; set; }
}

internal sealed class RemoveFromGroupResponse
{
	public string GroupName { get; set; }
	public List<User> Members { get; set; }
}
