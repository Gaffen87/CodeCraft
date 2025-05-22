namespace CodeCraftApi.Domain.DomainEvents;
/// <summary>
/// Represents an event that is triggered when code is submitted.
/// </summary>
/// <param name="ConnectionId"> The ID of the connection associated with the code submission.</param>
/// <param name="Code"> The code submitted.</param>
public record SessionRequestEvent(string ConnectionId, string Code) : IEvent { }
