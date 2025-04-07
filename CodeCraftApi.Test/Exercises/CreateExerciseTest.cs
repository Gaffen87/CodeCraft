using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.CreateExercise;
using FakeItEasy;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shouldly;

namespace CodeCraftApi.Test.Exercises;
public class CreateExerciseTest()
{
	[Fact]
	public async Task CreateExercise()
	{
		Exercise exercise = new Exercise();

		var fakeContext = A.Fake<AppDbContext>();
		var fakeDbSet = A.Fake<DbSet<Exercise>>();
		A.CallTo(() => fakeContext.Exercises).Returns(fakeDbSet);
		A.CallTo(() => fakeDbSet.Add(A<Exercise>.Ignored));


		var fakeConfig = A.Fake<IConfiguration>();
		A.CallTo(() => fakeConfig["tokenKey"]).Returns("Fake_Token_Signing_Secret");

		var endPoint = Factory.Create<CreateExerciseEndpoint>(fakeConfig);

		var req = new CreateExerciseRequest
		{

		};

		await endPoint.HandleAsync(req, default);
		var rsp = endPoint.Response;

		rsp.ShouldNotBeNull();
	}
}
