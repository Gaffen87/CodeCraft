namespace CodeCraftApi.Test.Exercises;

using Database;
using Domain.Entities;
using FastEndpoints;
using Features.Exercises.GetExerciseItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

public class GetExerciseItemTests
{
   

    [Fact]
    public async Task Get_Success()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid())
            .Options;

        await using var context = new AppDbContext(options);
        ArgumentException.ThrowIfNullOrEmpty(nameof(context));

        var itemToBeRetrieved = new ExerciseItem
        {
            Id = Guid.NewGuid(),
            Steps = [],
            Number = 1,
            Title = "Test Title"
        };

        context.ExerciseItem.Add(itemToBeRetrieved);
        await context.SaveChangesAsync();

        var req = new GetExerciseItemRequest
        {
            ExerciseItemId = itemToBeRetrieved.Id
        };

        var ep = Factory.Create<GetExerciseItemEndpoint>(
            ctx =>
            {
                ctx.AddTestServices(services =>
                {
                    services.AddScoped(_ => context);
                });
            }
        );

        var result = ep.HandleAsync(req, CancellationToken.None); 

        await result.ShouldNotBeNull();
    }
   
}