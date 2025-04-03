using FluentValidation;

namespace CodeCraftApi.Features.Tests.CreateTest;

internal sealed class Request
{
	public string Content { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }

	internal sealed class Validator : Validator<Request>
	{
		public Validator()
		{
			RuleFor(x => x.Content)
				.NotEmpty()
				.Must(x => x.StartsWith('{') && x.EndsWith('}')).WithMessage("Invalid JSON format");

			RuleFor(x => x.CreatedAt)
				.NotEmpty();

			RuleFor(x => x.UpdatedAt)
				.NotEmpty();
		}
	}
}

internal sealed class Response
{
	public Guid Id { get; set; }
	public string Content { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}
