namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using Shared;
/// <summary>
/// Summary class for the GetExerciseItem endpoint.
/// </summary>
internal sealed class Summary : EndpointSummary
{
    public Summary()
    {
        Summary = "Gets an Exercise item.";
        Description = "Gets an Exercise item.";
        
        ResponseExamples[201] = new GetExerciseItemResponse
        {
            Id = Guid.NewGuid(),
            Title = "Step 1",
            Number = 1,
            Steps =
            [
                new ExerciseStepResponse
                {
                    Id = Guid.NewGuid(),
                    Contraints = "none",
                    Description = "Step 1",
                    DescriptionShort = "Step 1 short",
                    Hints = "none gl hf",
                    Tests = [],
                    Title = "First step",
                }
            ]
        };
        Responses[403] = "Forbidden - insufficient permission";
    }
    
}