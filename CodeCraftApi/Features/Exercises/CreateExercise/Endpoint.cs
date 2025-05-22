using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.CreateExercise;
/// <summary>
/// Endpoint for creating a new exercise.
/// </summary>
/// <param name="context"> The application database context.</param>
internal sealed class CreateExerciseEndpoint(IAppDbContext context) : Endpoint<CreateExerciseRequest, CreateExerciseResponse, Mapper>
{
	/// <summary>
	/// Configures the endpoint.
	/// </summary>
	public override void Configure()
	{
		Post("");
		Group<ExerciseGroup>();
		Summary(new Summary());
	}
	/// <summary>
	/// Handles the request to create a new exercise.
	/// </summary>
	/// <param name="r"> The request containing the exercise data.</param>
	/// <param name="c"> The cancellation token.</param>
	public override async Task HandleAsync(CreateExerciseRequest r, CancellationToken c)
	{
		var exercise = Map.ToEntity(r);

		var createdExercise = await Data.CreateExerciseAsync(context, exercise);

		await SendCreatedAtAsync("GetExercise", new { ExerciseId = createdExercise.Id }, Map.FromEntity(createdExercise));
	}
}