using CodeCraftApi.Database;

namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;

internal sealed class Endpoint(AppDbContext dbContext) : Endpoint<Request, Response, Mapper>
{
	public override void Configure()
	{
		Get("/code/submissions/{GroupId}");
		Description(x => x.WithName("Get submissions by group id"));
		AllowAnonymous();
		Summary(new Summary());
	}

	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		var submissions = await Data.GetSubmissions(dbContext, r.GroupId);
		await SendAsync(Map.FromEntity(submissions));
	}
}