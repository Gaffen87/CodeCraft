using CodeCraftApi.Domain.Entities;
using FluentValidation;

namespace CodeCraftApi.Features.Exercises.GetProgress;

internal sealed class Request
{
	public Guid UserId { get; set; }

	internal sealed class Validator : Validator<Request>
	{
		public Validator()
		{
			RuleFor(x => x.UserId).NotEmpty();
		}
	}
}

internal sealed class Response
{
	public List<UserExerciseProgress> ExerciseProgress { get; set; } = [];
	public List<UserStepProgress> StepProgress { get; set; } = [];
}
