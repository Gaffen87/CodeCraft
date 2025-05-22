using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;
/// <summary>
/// Endpoint for retrieving code submissions by group ID.
/// </summary>
/// <param name="dbContext"> The application database context.</param>
internal sealed class Endpoint(IAppDbContext dbContext) : Endpoint<Request, Response, Mapper>
{
	/// <summary>
	/// Configures the endpoint.
	/// </summary>
	public override void Configure()
	{
		Get("/code/submissions/{GroupId}");
		Description(x => x.WithName("Get submissions by group id"));
		AllowAnonymous();
		Summary(new Summary());
	}
/// <summary>
/// Handles the request to retrieve code submissions by group ID.
/// </summary>
/// <param name="r"> The request containing the group ID.</param>
/// <param name="c"> The cancellation token.</param>
	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		var submissions = await Data.GetSubmissions(dbContext, r.GroupId);
		await SendAsync(Map.FromEntity(submissions));
	}
}