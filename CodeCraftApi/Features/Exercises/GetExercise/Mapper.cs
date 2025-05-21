using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.Shared;

namespace CodeCraftApi.Features.Exercises.GetExercise;

internal sealed class Mapper : Mapper<GetExerciseRequest, GetExerciseResponse, Exercise>
{
	public override GetExerciseResponse FromEntity(Exercise e)
	{
		GetExerciseResponse response = new()
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
				Contraints = item.Constraints,
				Hints = item.Hints,
				Tests = item.Tests
			};
			steps.Add(step);
		}

		return steps;
	}

}