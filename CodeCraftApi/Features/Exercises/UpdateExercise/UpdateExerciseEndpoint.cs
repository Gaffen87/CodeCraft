using CodeCraftApi.Database;

namespace CodeCraftApi.Features.Exercises.UpdateExercise;

internal sealed class UpdateExerciseEndpoint(AppDbContext context) : Endpoint<UpdateExerciseRequest, Response, Mapper>
{
	public override void Configure()
	{
		Put("/{ExerciseId}");
		Group<ExerciseGroup>();
		//TODO: Summary
	}

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