namespace CodeCraftApi.Domain.Entities;

public class Session
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public SessionStatus SessionStatus { get; set; }
}

public enum SessionStatus
{
	Active,
	Passive,
	Reconnecting
}
