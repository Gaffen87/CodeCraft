namespace CodeCraftApi.Domain.Events;

public class UserJoinedGroupEvent : IDomainEvent
{
	public string GroupName { get; set; }
	public Guid UserId { get; set; }
}
