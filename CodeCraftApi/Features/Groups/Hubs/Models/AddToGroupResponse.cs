using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Groups.Hubs.Models;

public class AddToGroupResponse
{
	public string GroupName { get; set; }
	public List<User> Members { get; set; }
}
