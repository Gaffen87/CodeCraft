using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Groups.AddToGroup;

internal sealed class AddToGroupPayload
{
	public string GroupName { get; set; }
}

internal sealed class AddToGroupResponse
{
	public string GroupName { get; set; }
	public List<User> Members { get; set; }
}
