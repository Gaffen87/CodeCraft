using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.ToggleVisibility;

internal sealed class Endpoint(IAppDbContext context) : Endpoint<Request>
{
	public override void Configure()
	{
		Patch("/toggle");
		Group<ExerciseGroup>();
	}

	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		await Data.ToggleVisibility(context, r);

		await SendNoContentAsync(c);
	}
}