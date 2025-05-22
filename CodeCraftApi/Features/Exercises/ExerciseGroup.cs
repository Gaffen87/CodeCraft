namespace CodeCraftApi.Features.Exercises;
/// <summary>
/// Group class for organizing exercise-related endpoints.
/// </summary>
internal sealed class ExerciseGroup : FastEndpoints.Group
{
	public ExerciseGroup()
	{
		Configure("/exercises", ep =>
		{
			ep.Description(b =>
				b.ProducesProblemDetails(401)
				.ProducesProblemDetails(403));
			ep.AllowAnonymous();
		});
	}
}
