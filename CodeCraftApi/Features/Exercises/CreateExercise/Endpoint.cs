using CodeCraftApi.Database;

namespace CodeCraftApi.Features.Exercises.CreateExercise;
internal sealed class CreateExerciseEndpoint(AppDbContext context) : Endpoint<CreateExerciseRequest, CreateExerciseResponse, Mapper>
{
	public override void Configure()
	{
		Post("");
		Group<ExerciseGroup>();
		Summary(new Summary());
	}
	public override async Task HandleAsync(CreateExerciseRequest r, CancellationToken c)
	{
		var exercise = Map.ToEntity(r);

		var createdExercise = await Data.CreateExerciseAsync(context, exercise);

		await SendCreatedAtAsync("GetExercise", new { ExerciseId = createdExercise.Id }, Map.FromEntity(createdExercise));
	}
}