using CodeCraftApi.Database;

namespace CodeCraftApi.Features.Exercises.CreateExercise;

using Domain.Entities;

internal sealed class EndpointWithMapping(AppDbContext context) : Endpoint<CreateExerciseRequest, CreateExerciseResponse, Mapper>
{
	public override void Configure()
	{
		Post("");
		AllowAnonymous();
		Description(b =>
			b.ProducesProblemDetails(400, "application/json")
				.ProducesProblemFE<InternalErrorResponse>(500));
		Group<ExerciseGroup>();
		Summary(new Summary());
	}
	public override async Task HandleAsync(CreateExerciseRequest r, CancellationToken c)
	{
		var exercise = Map.ToEntity(r);

		var createdExercise = await Data.CreateExerciseAsync(context, exercise);

		await SendAsync(Map.FromEntity(createdExercise));
	}

	private new sealed class Summary : EndpointSummary
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
			Responses[201] = "Exercise successfully created";
			Responses[403] = "Forbidden - insufficient permissions";
			
		}
	}
}