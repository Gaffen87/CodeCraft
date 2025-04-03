namespace CodeCraftApi.Domain.Entities;

public class Exercise
{
	public Guid Id { get; set; }
	public User? Author { get; set; }
	public string Title { get; set; }
	public string Summary { get; set; }
	public ExerciseDifficulty ExerciseDifficulty { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
	public bool IsDeleted { get; set; }
	public List<ExerciseItem> SubExercises { get; set; }
	public List<Category> Categories { get; set; }
	public List<Group> Groups { get; set; }
}

public enum ExerciseDifficulty
{
	Unassigned,
	Easy,
	Medium,
	Hard
}
