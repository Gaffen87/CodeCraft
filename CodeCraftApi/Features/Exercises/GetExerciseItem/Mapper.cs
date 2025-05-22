namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using Domain.Entities;
using Shared;
/// <summary>
/// Mapper class for converting between ExerciseItem and GetExerciseItemResponse.
/// </summary>
internal sealed class Mapper
    : Mapper<GetExerciseItemRequest, GetExerciseItemResponse, ExerciseItem>
{
    /// <summary>
    /// Maps an ExerciseItem entity to a GetExerciseItemResponse.
    /// </summary>
    /// <param name="eI"> The ExerciseItem entity to map.</param>
    /// <returns> The mapped GetExerciseItemResponse.</returns>
    public override GetExerciseItemResponse FromEntity(ExerciseItem eI)
    {
        GetExerciseItemResponse r = new()
        {
            Id = eI.Id,
            Title = eI.Title,
            Number = eI.Number,
            Steps = EntityToStepResponse(eI.Steps)

        };
        return r;
    }
    /// <summary>
    /// Maps a list of ExerciseStep entities to a list of ExerciseStepResponse.
    /// </summary>
    /// <param name="exerciseSteps"> The list of ExerciseStep entities to map.</param>
    /// <returns> The mapped list of ExerciseStepResponse.</returns>
    private static List<ExerciseStepResponse>? EntityToStepResponse(List<ExerciseStep> exerciseSteps)
    {
        return exerciseSteps?
            .Select(item => new ExerciseStepResponse
            {
                Id = Guid.NewGuid(),
                Title = item.Title,
                Description = item.Description,
                DescriptionShort = item.DescriptionShort,
                Contraints = item.Constraints,
                Hints = item.Hints,
                Tests = item.Tests
            })
            .ToList() ?? [];
    } 
}