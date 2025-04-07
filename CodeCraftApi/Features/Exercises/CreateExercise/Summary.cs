namespace CodeCraftApi.Features.Exercises.CreateExercise;

using CodeCraftApi.Features.Exercises.Shared;
using Domain.Entities;

internal sealed class Summary : EndpointSummary
{
	public Summary()
	{
		Summary = "Create a new exercise.";
		Description =
			"This endpoint creates a new exercise in the system. The request body must include the exercise title, summary, and difficulty level. " +
			"Upon success, the exercise details are returned.";
		ExampleRequest = new CreateExerciseRequest
		{
			Title = "Advanced C# Concepts",
			Summary = "This exercise covers advanced topics in C# such as async/await, LINQ, and delegates.",
			ExerciseDifficulty = ExerciseDifficulty.Medium,
			SubExercises =
			[
				new CreateExerciseItem
					{
						Number = 1,
						Title = "Understanding Async Programming",
						Steps =
						[
							new CreateExerciseStep
							{
								Title = "Step 1",
								Description = "Learn about asynchronous programming and its use cases.",
								DescriptionShort = "Intro to async/await.",
								Contraints = "Must include a working example.",
								Hints = "Focus on async Task and await keyword."
							}
						]
					}
			],
			Categories = [Guid.NewGuid(), Guid.NewGuid()],
			Groups = [Guid.NewGuid()]
		};

		Response(201, example: new CreateExerciseResponse
		{
			Id = Guid.NewGuid(),
			Title = "Advanced C# Concepts",
			Summary = "This exercise covers advanced topics in C# such as async/await, LINQ, and delegates.",
			ExerciseDifficulty = ExerciseDifficulty.Medium,
			SubExercises =
			[
				new ExerciseItemResponse
					{
						Id = Guid.NewGuid(),
						Number = 1,
						Title = "Understanding Async Programming",
						Steps =
						[
							new ExerciseStepResponse
							{
								Id = Guid.NewGuid(),
								Title = "Step 1",
								Description = "Learn about asynchronous programming and its use cases.",
								DescriptionShort = "Intro to async/await.",
								Contraints = "Must include a working example.",
								Hints = "Focus on async Task and await keyword."
							}
						]
					}
			],
			Categories = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
			Groups = new List<Guid> { Guid.NewGuid() }
		});
		Responses[403] = "Forbidden - insufficient permissions";

	}
}
