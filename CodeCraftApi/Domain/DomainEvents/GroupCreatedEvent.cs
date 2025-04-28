using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Domain.DomainEvents;

public record GroupCreatedEvent(Guid GroupId, string GroupName, List<User> Members, string Type = "created") : IEvent { }

