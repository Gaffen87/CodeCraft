namespace CodeCraftApi.Domain.Entities;

public class Category
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public List<Exercise> Exercises { get; set; }
}
