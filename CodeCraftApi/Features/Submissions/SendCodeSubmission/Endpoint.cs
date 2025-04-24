using Compiler;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
	public override void Configure()
	{
		Post("route");
	}

	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		string result = SimpleCompiler.HandleSubmission("hej");
		await SendAsync(new Response());
	}
}