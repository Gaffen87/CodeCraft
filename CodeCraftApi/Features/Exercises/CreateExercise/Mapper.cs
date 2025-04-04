using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Exercises.CreateExercise;

internal sealed class Mapper : Mapper<CreateExerciseRequest, CreateExerciseResponse, Exercise>
{
	public override Exercise ToEntity(CreateExerciseRequest r)
	{
		Exercise exercise = new()
		{
			Id = Guid.NewGuid(),
			Title = r.Title,
			Summary = r.Summary,
			ExerciseDifficulty = r.ExerciseDifficulty,
			Categories = [],
			Groups = [],
			SubExercises = ItemToEntity(r.SubExercises),
			CreatedAt = DateTimeOffset.UtcNow,
			UpdatedAt = DateTimeOffset.UtcNow,
		};
		return exercise;
	}

	public override CreateExerciseResponse FromEntity(Exercise e)
	{
		CreateExerciseResponse response = new()
		{
			Id = e.Id,
			Title = e.Title,
			Summary = e.Summary,
			ExerciseDifficulty = e.ExerciseDifficulty,
			Categories = [],
			Groups = [],
			SubExercises = EntityToItemResponse(e.SubExercises),
		};

		return response;
	}


	public static List<ExerciseItem> ItemToEntity(List<CreateExerciseItem> r)
	{
		List<ExerciseItem> items = [];

		foreach (var item in r)
		{
			ExerciseItem exerciseItem = new()
			{
				Id = Guid.NewGuid(),
				Title = item.Title,
				Number = item.Number,
				Steps = StepToEntity(item.Steps),
			};
			items.Add(exerciseItem);
		}

		return items;
	}

	public static List<ExerciseItemResponse> EntityToItemResponse(List<ExerciseItem> exerciseItems)
	{
		List<ExerciseItemResponse> items = [];

		foreach (var item in exerciseItems)
		{
			ExerciseItemResponse response = new()
			{
				Id = Guid.NewGuid(),
				Title = item.Title,
				Number = item.Number,
				Steps = EntityToStepResponse(item.Steps),
			};
			items.Add(response);
		}

		return items;
	}

	public static List<ExerciseStep> StepToEntity(List<CreateExerciseStep> r)
	{
		List<ExerciseStep> steps = [];

		foreach (var item in r)
		{
			ExerciseStep step = new()
			{
				Id = Guid.NewGuid(),
				Title = item.Title,
				Description = item.Description,
				DescriptionShort = item.DescriptionShort,
				Contraints = item.Contraints,
				Hints = item.Hints,
				Tests = []
			};
			steps.Add(step);
		}

		return steps;
	}

	public static List<ExerciseStepResponse> EntityToStepResponse(List<ExerciseStep> exerciseSteps)
	{
		List<ExerciseStepResponse> steps = [];

		foreach (var item in exerciseSteps)
		{
			ExerciseStepResponse step = new()
			{
				Id = Guid.NewGuid(),
				Title = item.Title,
				Description = item.Description,
				DescriptionShort = item.DescriptionShort,
				Contraints = item.Contraints,
				Hints = item.Hints,
				Tests = item.Tests
			};
			steps.Add(step);
		}

		return steps;
	}
}