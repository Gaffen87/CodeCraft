namespace CodeCraftApi.Domain.DomainEvents;

public record SessionRequestEvent(string ConnectionId, string Code) : IEvent { }
