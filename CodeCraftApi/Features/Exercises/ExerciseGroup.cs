namespace CodeCraftApi.Features.Exercises;

public class ExerciseGroup : FastEndpoints.Group
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
