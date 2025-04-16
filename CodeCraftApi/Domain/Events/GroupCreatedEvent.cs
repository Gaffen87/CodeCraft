namespace CodeCraftApi.Domain.Events;

public class GroupCreatedEvent : IDomainEvent
{
	public Guid Id { get; set; }
	public string Name { get; set; }
}
