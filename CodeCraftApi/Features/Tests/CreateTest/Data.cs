using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Tests.CreateTest;

internal sealed class Data
{
	public static Test CreateTest(AppDbContext context, Test test)
	{
		context.Tests.Add(test);
		context.SaveChanges();

		return test;
	}
}
