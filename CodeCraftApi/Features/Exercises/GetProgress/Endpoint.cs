using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.GetProgress;

internal sealed class Endpoint(IAppDbContext context) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/progress/{UserId}");
		Group<ExerciseGroup>();
	}

	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		var response = await Data.GetUserProgress(context, r.UserId);
		await SendAsync(response);
	}
}