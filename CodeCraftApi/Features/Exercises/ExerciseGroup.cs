namespace CodeCraftApi.Features.Exercises;

public class ExerciseGroup : Group
{
	public ExerciseGroup()
	{
		Configure("/exercises", ep =>
		{

		});
	}
}
