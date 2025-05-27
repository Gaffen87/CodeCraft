using FluentValidation;

namespace CodeCraftApi.Features.Exercises.ToggleVisibility;

internal sealed class Request
{
	public List<InnerRequest> Changes { get; set; }
}

internal sealed class InnerRequest
{
	public Guid ExerciseId { get; set; }
	public bool IsVisible { get; set; }

	internal sealed class Validator : Validator<InnerRequest>
	{
		public Validator()
		{
			RuleFor(x => x.ExerciseId).NotEmpty();
			RuleFor(x => x.IsVisible).NotNull();
		}
	}
}
