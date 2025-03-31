using FastEndpoints;

namespace CodeCraftApi.Features.Users.SignUp;

internal sealed class Endpoint : Endpoint<SignUpRequest, SignUpResponse, Mapper>
{
	public override void Configure()
	{
		Post("users");
	}

	public override async Task HandleAsync(SignUpRequest r, CancellationToken c)
	{
		await SendAsync(new SignUpResponse());
	}
}