namespace CodeCraftApi.Features.Groups.GetAllGroups;

using CodeCraftApi.Features.DbAbstraction;

internal sealed class GetAllGroupsEndpoint(IAppDbContext context) : EndpointWithoutRequest<GetAllGroupsResponse, Mapper>
{
	public override void Configure()
	{
		Get("/groups");
		Description(x => x.WithName("Get all groups"));
		AllowAnonymous();
		Summary(new Summary());

	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		var groups = await Data.GetGroupsAsync(context);

		if (groups == null)
		{
			await SendNotFoundAsync(ct);
		}
		else
		{
			await SendAsync(Map.FromEntity(groups), cancellation: ct);
		}
	}
}