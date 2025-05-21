using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.CreateExercise;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace CodeCraftApi.Test.Exercises;


public class CreateExerciseEndpointTests
{
	[Fact]
	public async Task Create_Success()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid())
			.Options;

		var context = new AppDbContext(options);

		var ep = Factory.Create<CreateExerciseEndpoint>(context);

		var req = new CreateExerciseRequest()
		{
			Title = "Title",
			Summary = "Test summary",
			ExerciseDifficulty = ExerciseDifficulty.Easy,
			SubExercises = []
		};

		try
		{
			await ep.HandleAsync(req, CancellationToken.None);
		}
		catch (InvalidOperationException ex) when (ex.Message.Contains("LinkGenerator"))
		{
		}

		var savedExercise = await context.Exercises.FirstOrDefaultAsync(e => e.Title == "Title");
		savedExercise.ShouldNotBeNull();
		savedExercise.Title.ShouldBe("Title");
		savedExercise.Summary.ShouldBe("Test summary");
		savedExercise.ExerciseDifficulty.ShouldBe(ExerciseDifficulty.Easy);
	}

}
