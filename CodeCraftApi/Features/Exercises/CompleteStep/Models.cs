using FluentValidation;

namespace CodeCraftApi.Features.Exercises.CompleteStep;

internal sealed class Request
{
	public Guid StepId { get; set; }
	public Guid UserId { get; set; }

	internal sealed class Validator : Validator<Request>
	{
		public Validator()
		{
			RuleFor(x => x.UserId).NotEmpty();
			RuleFor(x => x.StepId).NotEmpty();
		}
	}
}

internal sealed class Response
{
	public string Message => "Success";
}
