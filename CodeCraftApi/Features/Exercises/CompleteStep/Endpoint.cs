using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.CompleteStep;

internal sealed class Endpoint(IAppDbContext context) : Endpoint<Request>
{
	public override void Configure()
	{
		Post("/complete");
		Group<ExerciseGroup>();
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request r, CancellationToken c)
	{
		await Data.SetStepAsCompleted(context, r.StepId, r.UserId);
		await SendAsync(new Response());
	}
}