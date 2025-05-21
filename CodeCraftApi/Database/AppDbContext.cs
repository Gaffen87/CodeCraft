using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Database;

public class AppDbContext : DbContext, IAppDbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSnakeCaseNamingConvention();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Group>().HasMany(e => e.Exercises).WithMany(g => g.Groups)
			.UsingEntity<GroupExercise>();

		modelBuilder.Entity<Test>().Property(x => x.Content).HasColumnType("jsonb");
	}

	public DbSet<Exercise> Exercises { get; set; }
	public DbSet<ExerciseItem> ExerciseItem { get; set; }
	public DbSet<ExerciseStep> ExerciseStep { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Session> Sessions { get; set; }
	public DbSet<Test> Tests { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<CodeSubmission> Submissions { get; set; }
	public DbSet<CodeFile> CodeFiles { get; set; }
}
