namespace CodeCraftApi.Features.Exercises.UpdateExercise;

internal sealed class Endpoint : Endpoint<UpdateExerciseRequest, Response, Mapper>
{
	public override void Configure()
	{
		Post("route");
	}

	public override async Task HandleAsync(UpdateExerciseRequest r, CancellationToken c)
	{
		await SendAsync(new Response());
	}
}