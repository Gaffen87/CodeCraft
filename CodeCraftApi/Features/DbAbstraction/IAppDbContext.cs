using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Features.DbAbstraction;

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

	Task<int> SaveChangesAsync(CancellationToken ct = default);
}
