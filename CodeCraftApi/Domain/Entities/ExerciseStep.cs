namespace CodeCraftApi.Domain.Entities;

public class ExerciseStep
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string DescriptionShort { get; set; }
	public string Constraints { get; set; }
	public string Hints { get; set; }
	public List<Test> Tests { get; set; }
	public List<User> UserProgress { get; set; } = [];
}
