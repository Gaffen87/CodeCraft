namespace CodeCraftApi.Domain.Entities;

public class CodeSubmission
{
	public Guid Id { get; set; }
	public string Content { get; set; }
	public string Result { get; set; }
	public Group SubmittedBy { get; set; }
	public DateTimeOffset SubmitDate { get; set; }
	public bool IsSuccess { get; set; }
	public ExerciseStep ExerciseStep { get; set; }
}
