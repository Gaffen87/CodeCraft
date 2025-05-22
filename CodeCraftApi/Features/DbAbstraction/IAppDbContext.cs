using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.DbAbstraction;
/// <summary>
/// Interface for the application database context.
/// </summary>
public interface IAppDbContext
{
	DbSet<Exercise> Exercises { get; set; }
	DbSet<ExerciseItem> ExerciseItem { get; set; }
	DbSet<ExerciseStep> ExerciseStep { get; set; }
	DbSet<Group> Groups { get; set; }
	DbSet<Category> Categories { get; set; }
	DbSet<Session> Sessions { get; set; }
	DbSet<Test> Tests { get; set; }
	DbSet<User> Users { get; set; }
	DbSet<CodeSubmission> Submissions { get; set; }
	DbSet<CodeFile> CodeFiles { get; set; }
	/// <summary>
	/// Saves all changes made in this context to the database asynchronously.
	/// </summary>
	/// <param name="ct"> The cancellation token.</param>
	/// <returns> The number of state entries written to the database.</returns>
	Task<int> SaveChangesAsync(CancellationToken ct = default);
}
