using CodeCraftApi.Database;

namespace CodeCraftApi.Features.Tests.CreateTest;

internal sealed class Endpoint(AppDbContext context) : Endpoint<Request, Response, Mapper>
{
	public override void Configure()
	{
		Post("/tests");
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		var createdTest = Data.CreateTest(context, Map.ToEntity(r));

		await SendAsync(Map.FromEntity(createdTest));
	}
}