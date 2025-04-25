namespace CodeCraftApi.Domain.DomainEvents;

public record CodeSubmittedEvent(
	Guid SubmissionId,
	Guid GroupId,
	string GroupName,
	Guid ExerciseStepId,
	string CodeResult) : IEvent
{ }