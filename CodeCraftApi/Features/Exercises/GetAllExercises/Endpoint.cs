using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.GetAllExercises;

internal sealed class Endpoint(IAppDbContext context) : EndpointWithoutRequest<Response, Mapper>
{
	public override void Configure()
	{
		Get("");
		Group<ExerciseGroup>();
	}

	public override async Task HandleAsync(CancellationToken c)
	{
		var exercises = await Data.GetExercises(context);

		await SendAsync(new Response { Exercises = exercises });
	}
}