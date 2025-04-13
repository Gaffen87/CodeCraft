using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.Exercises.Shared;

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
		items.AddRange(r.Select(item => new ExerciseItem()
		{
			Id = Guid.NewGuid(), Title = item.Title, Number = item.Number, Steps = StepToEntity(item.Steps),
		}));

		return items;
	}

	public static List<ExerciseItemResponse> EntityToItemResponse(List<ExerciseItem> exerciseItems)
	{
		List<ExerciseItemResponse> items = [];
		items.AddRange(exerciseItems.Select(item => new ExerciseItemResponse()
		{
			Id = Guid.NewGuid(), Title = item.Title, Number = item.Number, Steps = EntityToStepResponse(item.Steps),
		}));

		return items;
	}

	public static List<ExerciseStep> StepToEntity(List<CreateExerciseStep> r)
	{
		List<ExerciseStep> steps = [];
		steps.AddRange(r.Select(item => new ExerciseStep()
		{
			Id = Guid.NewGuid(),
			Title = item.Title,
			Description = item.Description,
			DescriptionShort = item.DescriptionShort,
			Contraints = item.Contraints,
			Hints = item.Hints,
			Tests = []
		}));

		return steps;
	}

	public static List<ExerciseStepResponse> EntityToStepResponse(List<ExerciseStep> exerciseSteps)
	{
		List<ExerciseStepResponse> steps = [];
		steps.AddRange(exerciseSteps.Select(item => new ExerciseStepResponse()
		{
			Id = Guid.NewGuid(),
			Title = item.Title,
			Description = item.Description,
			DescriptionShort = item.DescriptionShort,
			Contraints = item.Contraints,
			Hints = item.Hints,
			Tests = item.Tests
		}));

		return steps;
	}
}