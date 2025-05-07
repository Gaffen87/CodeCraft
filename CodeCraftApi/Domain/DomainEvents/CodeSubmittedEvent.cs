namespace CodeCraftApi.Domain.DomainEvents;

public record CodeSubmittedEvent(
	Guid GroupId,
	string GroupName,
	string ExerciseStepTitle,
	string CodeResult,
	bool IsSuccess,
	DateTimeOffset TimeStamp) : IEvent
{ }