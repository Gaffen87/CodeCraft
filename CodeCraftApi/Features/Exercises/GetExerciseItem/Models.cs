namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using Domain.Entities;
using FluentValidation;
using Shared;

internal sealed class GetExerciseItemRequest
{
    public Guid ExerciseItemId { get; set; }

    internal sealed class Validator : Validator<GetExerciseItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.ExerciseItemId).NotEmpty();
        }
    }
}

internal sealed class GetExerciseItemResponse
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string? Title { get; set; }
    public List<ExerciseStepResponse>? Steps { get; set; }
}