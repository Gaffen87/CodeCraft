using CodeCraftApi.Database;

namespace CodeCraftApi.Features.Exercises.CreateExercise;

internal sealed class EndpointWithMapping(AppDbContext context) : Endpoint<CreateExerciseRequest, Response, Mapper>
{
	public override void Configure()
	{
		Post("");
		AllowAnonymous();
		Group<ExerciseGroup>();
	}

	public override async Task HandleAsync(CreateExerciseRequest r, CancellationToken c)
	{
		var exercise = Map.ToEntity(r);

		await SendAsync(new Response() { Message = "Success" });
	}
}