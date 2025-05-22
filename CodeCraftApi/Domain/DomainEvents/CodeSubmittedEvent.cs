namespace CodeCraftApi.Domain.DomainEvents;
/// <summary>
/// Represents an event that is triggered when code is submitted.
/// </summary>
/// <param name="GroupId"> The ID of the group associated with the code submission.</param>
/// <param name="GroupName"> The name of the group associated with the code submission.</param>
/// <param name="ExerciseStepTitle"> The title of the exercise step associated with the code submission.</param>
/// <param name="CodeResult"> The result of the code submission.</param>
/// <param name="IsSuccess"> Indicates whether the code submission was successful.</param>
/// <param name="TimeStamp"> The timestamp of when the code was submitted.</param>
public record CodeSubmittedEvent(
	Guid GroupId,
	string GroupName,
	string ExerciseStepTitle,
	string CodeResult,
	bool IsSuccess,
	DateTimeOffset TimeStamp) : IEvent
{ }