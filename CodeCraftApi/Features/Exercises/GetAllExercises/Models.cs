using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Exercises.GetAllExercises;

internal sealed class Response
{
	public List<Exercise> Exercises { get; set; }
}
