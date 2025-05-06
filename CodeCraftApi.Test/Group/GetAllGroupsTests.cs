namespace CodeCraftApi.Test.Group;

using Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shouldly;

public class GetAllGroupsTests
{
    [Fact]
    public async Task TestGetAllGroups()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid())
            .Options;

        await using var context = new AppDbContext(options);
        ArgumentException.ThrowIfNullOrEmpty(nameof(context));

        var group1 = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Group1",
            IsDeleted = false,
            IsActive = true,
            CreatedAt = DateTimeOffset.Now,
            Exercises = [],
            Members = [],
            UpdatedAt = DateTimeOffset.Now,

        };
        var group2 = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Group2",
            IsDeleted = false,
            IsActive = true,
            CreatedAt = DateTimeOffset.Now,
            Exercises = [],
            Members = [],
            UpdatedAt = DateTimeOffset.Now,

        }; 
       await context.Groups.AddRangeAsync(group1, group2);
       await context.SaveChangesAsync();
       
       var groups = await context.Groups.ToListAsync();
        groups.Count.ShouldBe(2);
    }
}