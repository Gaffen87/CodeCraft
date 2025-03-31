namespace CodeCraftApi.Domain.Entities;

public class User
{
	public Guid Id { get; set; }
	public Role Role { get; set; }
	public Status Status { get; set; }
	public string Email { get; set; }
	public string UserName { get; set; }
	public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
	public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
	public bool IsDeleted { get; set; }

	public List<Session> Sessions { get; set; } = [];
}

public enum Role
{
	Unassigned,
	Student,
	Teacher
}

public enum Status
{
	Active,
	Passive
}
