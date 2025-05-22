namespace CodeCraftApi.Features.Groups.GetAllGroups;

using CodeCraftApi.Features.DbAbstraction;
/// <summary>
/// Endpoint for retrieving all groups.
/// </summary>
/// <param name="context"> The application database context.</param>
internal sealed class GetAllGroupsEndpoint(IAppDbContext context) : EndpointWithoutRequest<GetAllGroupsResponse, Mapper>
{
	/// <summary>
	/// Configures the endpoint.
	/// </summary>
	public override void Configure()
	{
		Get("/groups");
		Description(x => x.WithName("Get all groups"));
		AllowAnonymous();
		Summary(new Summary());

	}
	/// <summary>
	/// Handles the request to retrieve all groups.
	/// </summary>
	/// <param name="ct"> The cancellation token.</param>
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