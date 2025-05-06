namespace CodeCraftApi.Test.Exercises;

using Database;
using Domain.Entities;
using FastEndpoints;
using Features.Exercises.UpdateExercise;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Microsoft.AspNetCore.Http.HttpResults;

public class UpdateExerciseTests
{
    [Fact]
    public async Task UpdateExercise_ShouldReturnUpdatedExercise()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid())
            .Options;
    
        var context = new AppDbContext(options);
        var firstExercise = new Exercise()
        {
            Id = Guid.NewGuid(),    
            Title = "Title",
            Summary = "Test summary",
            ExerciseDifficulty = ExerciseDifficulty.Easy,
            SubExercises = []
        }; 
        
        context.Exercises.Add(firstExercise);
        await context.SaveChangesAsync();
        
        // Create a test instance of the UpdateExerciseRequest
        var request = new UpdateExerciseRequest
        {
            ExerciseId = firstExercise.Id,
            Title = "Updated Title",
            Summary = "Updated summary",
            ExerciseDifficulty = ExerciseDifficulty.Medium,
            SubExercises = [] // Initialize with empty list to avoid null reference
        };
        
        var ep = Factory.Create<UpdateExerciseEndpoint>(
            ctx => {
                ctx.AddTestServices(s => {
                });
            },
            context);
        
        // Execute the endpoint
        await ep.HandleAsync(request, CancellationToken.None);
        
        // Ensure context changes are saved
        await context.SaveChangesAsync();
        
        // Verify the database was updated
        var updatedExercise = await context.Exercises.FindAsync(firstExercise.Id);
        
        updatedExercise.ShouldNotBeNull();
        updatedExercise.Title.ShouldBe("Updated Title");
        updatedExercise.Summary.ShouldBe("Updated summary");
        updatedExercise.ExerciseDifficulty.ShouldBe(ExerciseDifficulty.Medium);
       
    }
}