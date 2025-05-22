namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using Domain.Entities;
using FluentValidation;
using Shared;
/// <summary>
/// Request class for getting an exercise item.
/// </summary>
internal sealed class GetExerciseItemRequest
{
    public Guid ExerciseItemId { get; set; }

    /// <summary>
    /// Validator class for validating the request.
    /// </summary>
    internal sealed class Validator : Validator<GetExerciseItemRequest>
    {
        public Validator()
        {
            RuleFor(x => x.ExerciseItemId).NotEmpty();
        }
    }
}
/// <summary>
/// Response class for getting an exercise item.
/// </summary>
internal sealed class GetExerciseItemResponse
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string? Title { get; set; }
    public List<ExerciseStepResponse>? Steps { get; set; }
}