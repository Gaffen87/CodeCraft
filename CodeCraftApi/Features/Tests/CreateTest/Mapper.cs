using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Tests.CreateTest;

internal sealed class Mapper : Mapper<Request, Response, Test>
{
	public override Response FromEntity(Test t) => new()
	{
		Id = t.Id,
		Content = t.Content,
		CreatedAt = t.CreatedAt,
		UpdatedAt = t.UpdatedAt,
	};

	public override Test ToEntity(Request r) => new()
	{
		Id = Guid.NewGuid(),
		Content = r.Content,
		CreatedAt = r.CreatedAt,
		UpdatedAt = r.UpdatedAt,
	};
}