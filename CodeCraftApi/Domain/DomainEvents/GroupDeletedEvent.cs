namespace CodeCraftApi.Domain.DomainEvents;

public record GroupDeletedEvent(Guid GroupId, string Type = "deleted") : IEvent { }

