namespace CodeCraftApi.Features.Groups.GetAllGroups;

using Domain.Entities;
/// <summary>
/// Class representing the summary of the GetAllGroups endpoint.
/// </summary>
internal sealed class Summary : EndpointSummary
{
    public Summary()
    {
        Summary = "Get all groups";
        Description = "This endpoint is used to get all groups";

        ResponseExamples[201] = new GetAllGroupsResponse
        {
            Groups = new List<Group>
            {
                new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Group 1",
                    Members = new List<User>
                    {
                        new User
                        {
                            Id = Guid.NewGuid(),
                        }
                    },
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Id = Guid.NewGuid(),
                            Title = "Exercise 1",

                            SubExercises = new List<ExerciseItem>
                            {

                            },
                            Categories = new List<Category>
                            {
                                new Category
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "Category 1",
                                    Description = "Description 1",
                                    CreatedAt = DateTimeOffset.UtcNow,
                                }
                            }
                        }
                    },
                }
            },
        };
    }
}
                    
                
                
               
                
               
        