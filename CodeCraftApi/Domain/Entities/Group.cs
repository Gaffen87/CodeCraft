namespace CodeCraftApi.Domain.Entities;

public class Group
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
	public bool IsActive { get; set; }
	public bool IsDeleted { get; set; }
	public GroupSize GroupSize { get; set; }
	public List<User> Members { get; set; }
	public List<Exercise> Exercises { get; set; }
}

public enum GroupSize
{
	Team,
	Group,
	Pair
}
