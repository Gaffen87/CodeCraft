using CodeCraftApi.Database;

namespace CodeCraftApi.Features.Exercises.GetExercise;

internal sealed class GetExerciseEndpoint(AppDbContext context) : Endpoint<GetExerciseRequest, GetExerciseResponse, Mapper>
{
	public override void Configure()
	{
		Get("/{ExerciseId}");
		Group<ExerciseGroup>();
		Description(x => x.WithName("GetExercise"));
		Summary(new Summary());
	}

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