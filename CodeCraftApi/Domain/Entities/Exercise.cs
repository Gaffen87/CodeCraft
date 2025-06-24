namespace CodeCraftApi.Domain.Entities;

using System.Runtime.Serialization;

public class Exercise
{
	public Guid Id { get; set; }
	public string Title { get; set; }

	public string Summary { get; set; }
	public ExerciseDifficulty ExerciseDifficulty { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
	public bool IsDeleted { get; set; }
	public bool IsVisible { get; set; }
	public List<ExerciseItem> SubExercises { get; set; }
	public List<Category> Categories { get; set; }
	public List<Group> Groups { get; set; }
}

public enum ExerciseDifficulty
{
	[EnumMember(Value = "Unassigned")]
	Unassigned,
	[EnumMember(Value = "Easy")]
	Easy,
	[EnumMember(Value = "Medium")]
	Medium,
	[EnumMember(Value = "Hard")]
	Hard
}
