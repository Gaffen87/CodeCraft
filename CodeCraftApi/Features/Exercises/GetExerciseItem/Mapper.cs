namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using Domain.Entities;
using Shared;

internal sealed class Mapper
    : Mapper<GetExerciseItemRequest, GetExerciseItemResponse, ExerciseItem>
{
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

    private static List<ExerciseStepResponse>? EntityToStepResponse(List<ExerciseStep> exerciseSteps)
    {
        return exerciseSteps?
            .Select(item => new ExerciseStepResponse
            {
                Id = Guid.NewGuid(),
                Title = item.Title,
                Description = item.Description,
                DescriptionShort = item.DescriptionShort,
                Contraints = item.Contraints,
                Hints = item.Hints,
                Tests = item.Tests
            })
            .ToList() ?? [];
    } 
}