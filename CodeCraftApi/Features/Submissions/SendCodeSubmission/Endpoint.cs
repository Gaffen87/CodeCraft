using Compiler;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
	public override void Configure()
	{
		Post("/codesubmission");
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		var result = await SimpleCompiler.CodeRunner(r.CodeRequest);
		await SendAsync(new Response { Result = result });
	}
}