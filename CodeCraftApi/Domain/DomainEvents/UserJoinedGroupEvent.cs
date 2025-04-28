using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Domain.DomainEvents;

public record UserJoinedGroupEvent(Guid GroupId, string GroupName, List<User> User, string Type = "joined") : IEvent { }