namespace CodeCraftApi.Domain.Events;

public class UserLeftGroupEvent : IDomainEvent
{
	public string GroupName { get; set; }
	public Guid UserId { get; set; }
}
