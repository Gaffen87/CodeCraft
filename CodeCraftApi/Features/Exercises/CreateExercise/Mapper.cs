using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.Shared;

namespace CodeCraftApi.Features.Exercises.CreateExercise;
/// <summary>
/// Mapper class for creating exercises.
/// </summary>
internal sealed class Mapper : Mapper<CreateExerciseRequest, CreateExerciseResponse, Exercise>
{
	/// <summary>
	/// Maps a CreateExerciseRequest to an Exercise entity.
	/// </summary>
	/// <param name="r"> The CreateExerciseRequest to map.</param>
	/// <returns> The mapped Exercise entity.</returns>
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
	/// <summary>
	/// Maps an Exercise entity to a CreateExerciseResponse.
	/// </summary>
	/// <param name="e"> The Exercise entity to map.</param>
	/// <returns> The mapped CreateExerciseResponse.</returns>
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

	/// <summary>
	/// Maps a list of CreateExerciseItem to a list of ExerciseItem entities.
	/// </summary>
	/// <param name="r"> The list of CreateExerciseItem to map.</param>
	/// <returns> The mapped list of ExerciseItem entities.</returns>
	private static List<ExerciseItem> ItemToEntity(List<CreateExerciseItem> r)
	{
		List<ExerciseItem> items = [];
		items.AddRange(r.Select(item => new ExerciseItem()
		{
			Id = Guid.NewGuid(),
			Title = item.Title,
			Number = item.Number,
			Steps = StepToEntity(item.Steps),
		}));

		return items;
	}
	/// <summary>
	/// Maps a list of ExerciseItem entities to a list of ExerciseItemResponse.
	/// </summary>
	/// <param name="exerciseItems"> The list of ExerciseItem entities to map.</param>
	/// <returns> The mapped list of ExerciseItemResponse.</returns>
	private static List<ExerciseItemResponse> EntityToItemResponse(List<ExerciseItem> exerciseItems)
	{
		List<ExerciseItemResponse> items = [];
		items.AddRange(exerciseItems.Select(item => new ExerciseItemResponse()
		{
			Id = Guid.NewGuid(),
			Title = item.Title,
			Number = item.Number,
			Steps = EntityToStepResponse(item.Steps),
		}));

		return items;
	}
	/// <summary>
	/// Maps a list of CreateExerciseStep to a list of ExerciseStep entities.
	/// </summary>
	/// <param name="r"> The list of CreateExerciseStep to map.</param>
	/// <returns> The mapped list of ExerciseStep entities.</returns>
	private static List<ExerciseStep> StepToEntity(List<CreateExerciseStep> r)
	{
		List<ExerciseStep> steps = [];
		steps.AddRange(r.Select(item => new ExerciseStep()
		{
			Id = Guid.NewGuid(),
			Title = item.Title,
			Description = item.Description,
			DescriptionShort = item.DescriptionShort,
			Constraints = item.Constraints,
			Hints = item.Hints,
			Tests = []
		}));

		return steps;
	}
	/// <summary>
	/// Maps a list of ExerciseStep entities to a list of ExerciseStepResponse.
	/// </summary>
	/// <param name="exerciseSteps"> The list of ExerciseStep entities to map.</param>
	/// <returns> The mapped list of ExerciseStepResponse.</returns>
	private static List<ExerciseStepResponse> EntityToStepResponse(List<ExerciseStep> exerciseSteps)
	{
		List<ExerciseStepResponse> steps = [];
		steps.AddRange(exerciseSteps.Select(item => new ExerciseStepResponse()
		{
			Id = Guid.NewGuid(),
			Title = item.Title,
			Description = item.Description,
			DescriptionShort = item.DescriptionShort,
			Contraints = item.Constraints,
			Hints = item.Hints,
			Tests = item.Tests
		}));

		return steps;
	}
}