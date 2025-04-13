namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using Database;

internal sealed class GetExerciseItemEndpoint(AppDbContext context) 
    : Endpoint<GetExerciseItemRequest,GetExerciseItemResponse, Mapper>
{
    public override void Configure()
    {
        Get("/Item/{ExerciseItemId}");
        Group<ExerciseGroup>();
        Description(x =>  x.WithName("Get Exercise Item"));
        Summary(new Summary());
    }

    public override async Task HandleAsync(GetExerciseItemRequest req, CancellationToken ct)
    {
        var exerciseItem = await Data.GetExerciseItemsAsync(context, req.ExerciseItemId);

        if (exerciseItem == null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            await SendAsync(Map.FromEntity(exerciseItem), cancellation: ct);
        }
    }
}