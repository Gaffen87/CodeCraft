using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Exercises.UpdateExercise;
/// <summary>
/// Mapper class for converting UpdateExerciseRequest to Exercise entity.
/// </summary>
internal sealed class Mapper : Mapper<UpdateExerciseRequest, Response, Exercise>
{
	/// <summary>
	/// Converts an UpdateExerciseRequest to an Exercise entity.
	/// </summary>
	/// <param name="r"> The UpdateExerciseRequest to convert.</param>
	/// <returns> The converted Exercise entity.</returns>
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
	/// <summary>
	/// Converts an Exercise entity to an UpdateExerciseResponse.
	/// </summary>
	/// <param name="r"> The Exercise entity to convert.</param>
	/// <returns> The converted UpdateExerciseResponse.</returns>
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
	/// <summary>
	/// Converts a list of UpdateExerciseStep to a list of ExerciseStep entities.
	/// </summary>
	/// <param name="r"> The list of UpdateExerciseStep to convert.</param>
	/// <returns>> The converted list of ExerciseStep entities.</returns>
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
				Constraints = item.Contraints,
				Hints = item.Hints,
				Tests = []
			};
			steps.Add(step);
		}

		return steps;
	}

	//TODO: tilføj test mapping
}