namespace CodeCraftApi.Features.Submissions.GetSubmissionsByGroupId;

internal sealed class Summary : EndpointSummary
{
    public Summary()
    {
        Summary = "Get submissions by group id";
        Description = "This endpoint is used to get submissions by group id";
        
        ResponseExamples[200] = new Response 
        {
            Submissions =
            [
                new Submission
                {
                    Content = "Submission content",
                    GroupName = "Group name",
                    TimeStamp = DateTimeOffset.UtcNow,
                    IsSuccess = true
                }
            ],
        };
    }
    
}