using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Domain.DomainEvents;
/// <summary>
/// Represents an event that is triggered when a group is created.
/// </summary>
/// <param name="GroupId"> The ID of the group.</param>
/// <param name="GroupName"> The name of the group.</param>
/// <param name="Members"> The list of members in the group.</param>
/// <param name="Type"> The type of the event. Default is "created".</param>
public record GroupCreatedEvent(Guid GroupId, string GroupName, List<User> Members, string Type = "created") : IEvent { }

