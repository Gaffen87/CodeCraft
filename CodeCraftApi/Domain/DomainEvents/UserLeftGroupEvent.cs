namespace CodeCraftApi.Domain.DomainEvents;

public record UserLeftGroupEvent(Guid GroupId, Guid UserId, string Type = "left") : IEvent { }
