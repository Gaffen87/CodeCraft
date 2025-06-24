using System.Text.Json.Serialization;

namespace CodeCraftApi.Domain.Entities;

public class UserExerciseProgress
{
	public Guid Id { get; set; }
	public Exercise Exercise { get; set; }
	[JsonIgnore] public User User { get; set; }
	public bool Completed { get; set; }
}
