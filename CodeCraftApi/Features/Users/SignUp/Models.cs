using FastEndpoints;
using FluentValidation;

namespace CodeCraftApi.Features.Users.SignUp;

internal sealed class SignUpRequest
{
	public string Email { get; set; }
	public string Password { get; set; }

	internal sealed class Validator : Validator<SignUpRequest>
	{
		public Validator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("No email provided")
				.EmailAddress().WithMessage("Must be a valid email");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("No password provided")
				.MinimumLength(8).WithMessage("Password must be at least 8 characters");
		}
	}
}

internal sealed class SignUpResponse
{
	public string Message => "This endpoint hasn't been implemented yet!";
}
