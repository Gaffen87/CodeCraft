using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.GetExercise;
/// <summary>
/// Endpoint for retrieving an exercise by its ID.
/// </summary>
/// <param name="context"> The application database context.</param>
internal sealed class GetExerciseEndpoint(IAppDbContext context) : Endpoint<GetExerciseRequest, GetExerciseResponse, Mapper>
{
	/// <summary>
	/// Configures the endpoint.
	/// </summary>
	public override void Configure()
	{
		Get("/{ExerciseId}");
		Group<ExerciseGroup>();
		Description(x => x.WithName("GetExercise"));
		Summary(new Summary());
		Roles("teacher", "student");
	}
	/// <summary>
	/// Handles the request to retrieve an exercise by its ID.
	/// </summary>
	/// <param name="r"> The request containing the exercise ID.</param>
	/// <param name="c"> The cancellation token.</param>
	public override async Task HandleAsync(GetExerciseRequest r, CancellationToken c)
	{
		var exercise = await Data.GetExerciseAsync(context, r.ExerciseId);

		if (exercise == null)
		{
			await SendNotFoundAsync();
		}
		else
		{
			await SendAsync(Map.FromEntity(exercise));
		}
	}
}