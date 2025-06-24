using System.Text.Json.Serialization;

namespace CodeCraftApi.Domain.Entities;

public class UserStepProgress
{
	public Guid Id { get; set; }
	public ExerciseStep ExerciseStep { get; set; }
	[JsonIgnore] public User User { get; set; }
	public bool Completed { get; set; }
}
