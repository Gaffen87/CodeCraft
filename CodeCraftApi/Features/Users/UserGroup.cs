using FastEndpoints;

namespace CodeCraftApi.Features.Users;

public class UserGroup : Group
{
	public UserGroup()
	{
		Configure("users", ep =>
		{
			ep.Description(x => x.AllowAnonymous());
		});
	}
}
