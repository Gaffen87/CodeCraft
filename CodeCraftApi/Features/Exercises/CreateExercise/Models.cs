using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.Shared;
using FluentValidation;

namespace CodeCraftApi.Features.Exercises.CreateExercise;
/// <summary>
/// Request class for creating exercises.
/// </summary>
internal sealed class CreateExerciseRequest
{
	/// <summary>
	/// The title of the exercise.
	/// </summary>
	public string Title { get; set; }
	/// <summary>
	/// The summary of the exercise.
	/// </summary>
	public string Summary { get; set; }
	/// <summary>
	/// The difficulty level of the exercise.
	/// </summary>
	public ExerciseDifficulty ExerciseDifficulty { get; set; }
	/// <summary>
	/// The list of sub-exercises.
	/// </summary>
	public List<CreateExerciseItem> SubExercises { get; set; }
	/// <summary>
	/// The list of categories associated with the exercise.
	/// </summary>
	public List<Guid> Categories { get; set; }
	/// <summary>
	/// The list of groups associated with the exercise.
	/// </summary>
	public List<Guid> Groups { get; set; }

	/// <summary>
	/// Validates the request.
	/// </summary>
	internal sealed class Validator : Validator<CreateExerciseRequest>
	{
		public Validator()
		{
			RuleFor(x => x.ExerciseDifficulty)
				.NotEmpty()
				.IsInEnum();

			RuleFor(x => x.Title)
				.NotEmpty();

			RuleFor(x => x.Summary)
				.NotEmpty();
		}
	}
}
/// <summary>
/// Response class for creating exercises.
/// </summary>
internal sealed class CreateExerciseResponse
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Summary { get; set; }
	public ExerciseDifficulty ExerciseDifficulty { get; set; }
	public List<ExerciseItemResponse> SubExercises { get; set; }
	public List<Guid> Categories { get; set; }
	public List<Guid> Groups { get; set; }
}
/// <summary>
/// Represents an item in the exercise.
/// </summary>
internal sealed class CreateExerciseItem()
{
	public int Number { get; set; }
	public string Title { get; set; }
	public List<CreateExerciseStep> Steps { get; set; }
}
/// <summary>
/// Represents a step in the exercise item.
/// </summary>
internal sealed class CreateExerciseStep()
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string DescriptionShort { get; set; }
	public string Constraints { get; set; }
	public string Hints { get; set; }
}
