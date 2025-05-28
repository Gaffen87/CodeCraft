namespace CodeCraftApi.Features.Groups.SignalR.UpdateExercise;

internal sealed class UpdateExercisePayload
{
	public Guid ExerciseStepId { get; set; }
	public Guid GroupId { get; set; }
}
