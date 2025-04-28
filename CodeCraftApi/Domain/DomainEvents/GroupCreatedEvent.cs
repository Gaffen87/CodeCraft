namespace CodeCraftApi.Domain.DomainEvents;

public record GroupCreatedEvent(string GroupName) : IEvent { }

