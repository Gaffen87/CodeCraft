using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Database;

public class AppDbContext : DbContext
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

	public virtual DbSet<Exercise> Exercises { get; set; }
	public virtual DbSet<ExerciseItem> ExerciseItem { get; set; }
	public virtual DbSet<ExerciseStep> ExerciseStep { get; set; }
	public virtual DbSet<Group> Groups { get; set; }
	public virtual DbSet<Category> Categories { get; set; }
	public virtual DbSet<Session> Sessions { get; set; }
	public virtual DbSet<Test> Tests { get; set; }
	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<CodeSubmission> Submissions { get; set; }
	public virtual DbSet<CodeFile> CodeFiles { get; set; }
}
