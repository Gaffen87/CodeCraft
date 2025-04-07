using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Exercises.Shared;

internal sealed class ExerciseItemResponse()
{
	public Guid Id { get; set; }
	public int Number { get; set; }
	public string Title { get; set; }
	public List<ExerciseStepResponse> Steps { get; set; }
}

internal sealed class ExerciseStepResponse()
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string DescriptionShort { get; set; }
	public string Contraints { get; set; }
	public string Hints { get; set; }
	public List<Test> Tests { get; set; }
}

