using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.Shared;

namespace CodeCraftApi.Features.Exercises.GetExercise;
/// <summary>
/// Mapper class for converting between Exercise entities and GetExerciseResponse.
/// </summary>
internal sealed class Mapper : Mapper<GetExerciseRequest, GetExerciseResponse, Exercise>
{
	/// <summary>
	/// Maps a GetExerciseRequest to an Exercise entity.
	/// </summary>
	/// <param name="e"> The GetExerciseRequest to map.</param>
	/// <returns></returns>
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
	/// <summary>
	/// Maps a list of ExerciseItem entities to a list of ExerciseItemResponse.
	/// </summary>
	/// <param name="exerciseItems"> The list of ExerciseItem entities to map.</param>
	/// <returns> The mapped list of ExerciseItemResponse.</returns>
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
	/// <summary>
	/// Maps a list of ExerciseStep entities to a list of ExerciseStepResponse.
	/// </summary>
	/// <param name="exerciseSteps"> The list of ExerciseStep entities to map.</param>
	/// <returns> The mapped list of ExerciseStepResponse.</returns>
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