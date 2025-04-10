using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Exercises.UpdateExercise;

internal sealed class Mapper : Mapper<UpdateExerciseRequest, Response, Exercise>
{
	public override Exercise ToEntity(UpdateExerciseRequest r)
	{
		Exercise exercise = new()
		{
			Id = r.ExerciseId,
			Title = r.Title,
			Summary = r.Summary,
			ExerciseDifficulty = r.ExerciseDifficulty,
			SubExercises = ItemToEntity(r.SubExercises),
			CreatedAt = DateTimeOffset.UtcNow,
			UpdatedAt = DateTimeOffset.UtcNow,
		};
		return exercise;
	}

	public static List<ExerciseItem> ItemToEntity(List<UpdateExerciseItem> r)
	{
		List<ExerciseItem> items = [];

		foreach (var item in r)
		{
			ExerciseItem exerciseItem = new()
			{
				Id = (Guid)item.Id!,
				Title = item.Title,
				Number = item.Number,
				Steps = StepToEntity(item.Steps),
			};
			items.Add(exerciseItem);
		}

		return items;
	}

	public static List<ExerciseStep> StepToEntity(List<UpdateExerciseStep> r)
	{
		List<ExerciseStep> steps = [];

		foreach (var item in r)
		{
			ExerciseStep step = new()
			{
				Id = (Guid)item.Id!,
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

	//TODO: tilføj test mapping
}