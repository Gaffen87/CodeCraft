using CodeCraftApi.Domain.Entities;
using FluentValidation;

namespace CodeCraftApi.Features.Exercises.CreateExercise;

internal sealed class CreateExerciseRequest
{
	public string Title { get; set; }
	public string Summary { get; set; }
	public ExerciseDifficulty ExerciseDifficulty { get; set; }
	public List<CreateExerciseItem> SubExercises { get; set; }
	public List<Guid> Categories { get; set; }
	public List<Guid> Groups { get; set; }

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

internal sealed class CreateExerciseItem()
{
	public int Number { get; set; }
	public string Title { get; set; }
	public List<CreateExerciseStep> Steps { get; set; }
}

internal sealed class CreateExerciseStep()
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string DescriptionShort { get; set; }
	public string Contraints { get; set; }
	public string Hints { get; set; }
}


internal sealed class Response
{
	public string Message;
}
