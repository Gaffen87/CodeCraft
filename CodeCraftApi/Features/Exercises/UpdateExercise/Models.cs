using CodeCraftApi.Domain.Entities;
using FluentValidation;

namespace CodeCraftApi.Features.Exercises.UpdateExercise;

internal sealed class UpdateExerciseRequest
{
	public Guid ExerciseId { get; set; }
	public string Title { get; set; }
	public string Summary { get; set; }
	public ExerciseDifficulty ExerciseDifficulty { get; set; }
	public List<UpdateExerciseItem> SubExercises { get; set; }
	public List<Guid> Categories { get; set; }
	public List<Guid> Groups { get; set; }

	internal sealed class Validator : Validator<UpdateExerciseRequest>
	{
		public Validator()
		{
			RuleFor(x => x.ExerciseId).NotEmpty();

			RuleFor(x => x.ExerciseDifficulty)
				.NotEmpty()
				.IsInEnum();

			RuleFor(x => x.Title)
				.NotEmpty();

			RuleFor(x => x.Summary)
				.NotEmpty();

			RuleForEach(x => x.SubExercises)
				.SetValidator(new UpdateExerciseItem.Validator());
		}
	}
}

internal sealed class UpdateExerciseItem()
{
	public Guid ExerciseItemId { get; set; }
	public int Number { get; set; }
	public string Title { get; set; }
	public List<UpdateExerciseStep> Steps { get; set; }

	internal sealed class Validator : Validator<UpdateExerciseItem>
	{
		public Validator()
		{
			RuleFor(x => x.ExerciseItemId).NotEmpty();

			RuleFor(x => x.Number)
				.NotEmpty();

			RuleFor(x => x.Title).NotEmpty();

			RuleForEach(x => x.Steps)
				.SetValidator(new UpdateExerciseStep.Validator());
		}
	}
}

internal sealed class UpdateExerciseStep()
{
	public Guid ExerciseStepId { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string DescriptionShort { get; set; }
	public string Contraints { get; set; }
	public string Hints { get; set; }
	public List<UpdateTestItem> Tests { get; set; }

	internal sealed class Validator : Validator<UpdateExerciseStep>
	{
		public Validator()
		{
			RuleFor(x => x.ExerciseStepId).NotEmpty();

			RuleFor(x => x.Title).NotEmpty();
			RuleFor(x => x.Description).NotEmpty();
			RuleFor(x => x.DescriptionShort).NotEmpty();
			RuleFor(x => x.Contraints).NotNull();
			RuleFor(x => x.Hints).NotNull();

			RuleForEach(x => x.Tests)
				.SetValidator(new UpdateTestItem.Validator());
		}
	}
}


internal sealed class UpdateTestItem()
{
	public Guid TestId { get; set; }
	public string Content { get; set; }

	internal sealed class Validator : Validator<UpdateTestItem>
	{
		public Validator()
		{
			RuleFor(x => x.TestId).NotEmpty();

			RuleFor(x => x.Content).NotEmpty();
		}
	}
}

internal sealed class Response
{
	public string Message => "This endpoint hasn't been implemented yet!";
}
