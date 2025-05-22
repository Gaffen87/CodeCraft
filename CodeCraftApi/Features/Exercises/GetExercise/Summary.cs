using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.CreateExercise;
using CodeCraftApi.Features.Exercises.Shared;

namespace CodeCraftApi.Features.Exercises.GetExercise;
/// <summary>
/// Summary class for the GetExercise endpoint.
/// </summary>
internal sealed class Summary : EndpointSummary
{
	public Summary()
	{
		Summary = "Get an exercise.";
		Description =
			"This endpoint retrieves an exercise in the system. The request body must include the exercise Id. " +
			"Upon success, the exercise details are returned.";

		ResponseExamples[200] = new CreateExerciseResponse
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
		};
		Responses[403] = "Forbidden - insufficient permissions";

	}
}
