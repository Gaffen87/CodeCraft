using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.Shared;
using FluentValidation;

namespace CodeCraftApi.Features.Exercises.GetExercise;

internal sealed class GetExerciseRequest
{
	public Guid ExerciseId { get; set; }

	internal sealed class Validator : Validator<GetExerciseRequest>
	{
		public Validator()
		{
			RuleFor(x => x.ExerciseId).NotEmpty();
		}
	}
}

internal sealed class GetExerciseResponse
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Summary { get; set; }
	public ExerciseDifficulty ExerciseDifficulty { get; set; }
	public List<ExerciseItemResponse> SubExercises { get; set; }
	public List<Guid> Categories { get; set; }
	public List<Guid> Groups { get; set; }
}
