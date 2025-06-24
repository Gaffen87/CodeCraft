using CodeCraftApi.Features.DbAbstraction;

namespace CodeCraftApi.Features.Exercises.UpdateExercise;
/// <summary>
/// Endpoint for updating an exercise.
/// </summary>
/// <param name="context"> The application database context.</param>
internal sealed class UpdateExerciseEndpoint(IAppDbContext context) : Endpoint<UpdateExerciseRequest, Response, Mapper>
{
	/// <summary>
	/// Configures the endpoint.
	/// </summary>
	public override void Configure()
	{
		Put("/{ExerciseId}");
		Group<ExerciseGroup>();
		Roles("teacher");
		//TODO: Summary
	}
	/// <summary>
	/// Handles the request to update an exercise.
	/// </summary>
	/// <param name="r"> The request containing the exercise data.</param>
	/// <param name="c"> The cancellation token.</param>
	public override async Task HandleAsync(UpdateExerciseRequest r, CancellationToken c)
	{
		if (await Data.ExerciseExists(context, r.ExerciseId) == false)
		{
			await SendNotFoundAsync();
		}
		else
		{
			var result = await Data.UpdateExercise(context, Map.ToEntity(r));

			if (!result)
			{
				await SendResultAsync(TypedResults.BadRequest());
			}
			else
			{
				await SendAsync(new Response());
			}
		}
	}
}