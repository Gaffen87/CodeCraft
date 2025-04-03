namespace CodeCraftApi.Domain.Entities;

public class ExerciseItem
{
	public Guid Id { get; set; }
	public int Number { get; set; }
	public string Title { get; set; }
	public List<ExerciseStep> Steps { get; set; }
}
