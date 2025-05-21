using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;

internal sealed class Endpoint(IAppDbContext dbContext) : Endpoint<Request, Response, Mapper>
{
	public override void Configure()
	{
		Get("/code/submissions/{GroupId}");
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		var submissions = await Data.GetSubmissions(dbContext, r.GroupId);
		await SendAsync(Map.FromEntity(submissions));
	}
}