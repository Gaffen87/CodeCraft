using CodeCraftApi.Database;

namespace CodeCraftApi.Features.Exercises.CreateExercise;

internal sealed class EndpointWithMapping(AppDbContext context) : Endpoint<CreateExerciseRequest, CreateExerciseResponse, Mapper>
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

		var createdExercise = await Data.CreateExerciseAsync(context, exercise);

		await SendAsync(Map.FromEntity(createdExercise));
	}
}