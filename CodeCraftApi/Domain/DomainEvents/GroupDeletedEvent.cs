namespace CodeCraftApi.Domain.DomainEvents;
/// <summary>
/// Represents an event that is triggered when code is submitted.
/// </summary>
/// <param name="GroupId"> The ID of the group associated with the code submission.</param>
/// <param name="Type"> The type of the event. Default is "deleted".</param>
public record GroupDeletedEvent(Guid GroupId, string Type = "deleted") : IEvent { }

