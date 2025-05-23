namespace CodeCraftApi.Test.Exercises;

using Database;
using Domain.Entities;
using FastEndpoints;
using Features.Exercises.GetExercise;
using Microsoft.EntityFrameworkCore;
using Shouldly;

public class GetExerciseTests
{
	[Fact]
	public async Task GetExercise_ValidInput_ShouldReturnExercise()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "testData_" + Guid.NewGuid())
			.Options;
		await using var context = new AppDbContext(options);

		var exerciseToBeRetrieved = new Exercise
		{
			Id = Guid.NewGuid(),
			Title = "Test Exercise",
			Summary = "Test exercise",
			CreatedAt = DateTimeOffset.Now,
			IsDeleted = false


		};

		context.Exercises.Add(exerciseToBeRetrieved);
		await context.SaveChangesAsync();



		var req = new GetExerciseRequest
		{
			ExerciseId = exerciseToBeRetrieved.Id
		};
		var ep = Factory.Create<GetExerciseEndpoint>(context);

		var result = ep.HandleAsync(req, CancellationToken.None);

		await result.ShouldNotBeNull();
	}

}