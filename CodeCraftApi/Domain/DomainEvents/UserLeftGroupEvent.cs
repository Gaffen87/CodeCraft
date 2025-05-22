namespace CodeCraftApi.Domain.DomainEvents;
/// <summary>
/// Represents an event that is triggered when code is submitted.
/// </summary>
/// <param name="GroupId"> The ID of the group associated with the code submission.</param>
/// <param name="UserId"> The ID of the user who submitted the code.</param>
/// <param name="Type"> The type of event. Default is "left".</param>
public record UserLeftGroupEvent(Guid GroupId, Guid UserId, string Type = "left") : IEvent { }
