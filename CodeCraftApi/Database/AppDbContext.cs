using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;
using Group = CodeCraftApi.Domain.Entities.Group;

namespace CodeCraftApi.Database;
/// <summary>
/// The main database context for the application.
/// </summary>
public class AppDbContext : DbContext, IAppDbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}
	/// <summary>
	/// Configures the database context.
	/// </summary>
	/// <param name="optionsBuilder">instance of DbContextOptionsBuilder</param>
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSnakeCaseNamingConvention();
	/// <summary>
	/// Configures the model for the database context.
	/// </summary>
	/// <param name="modelBuilder">instance of ModelBuilder</param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Group>().HasMany(e => e.Exercises).WithMany(g => g.Groups)
			.UsingEntity<GroupExercise>();

		modelBuilder.Entity<Test>().Property(x => x.Content).HasColumnType("jsonb");
	}
	/// <summary>
	/// DbSet for the Exercise entity.
	/// </summary>
	public DbSet<Exercise> Exercises { get; set; }
	/// <summary>
	/// DbSet for the ExerciseItem entity.
	/// </summary>
	public DbSet<ExerciseItem> ExerciseItem { get; set; }
	/// <summary>
	/// DbSet for the ExerciseStep entity.
	/// </summary>
	public DbSet<ExerciseStep> ExerciseStep { get; set; }
	/// <summary>
	/// DbSet for the Group entity.
	/// </summary>
	public DbSet<Group> Groups { get; set; }
	/// <summary>
	/// DbSet for the Category entity.
	/// </summary>
	public DbSet<Category> Categories { get; set; }
	/// <summary>
	/// DbSet for the Session entity.
	/// </summary>
	public DbSet<Session> Sessions { get; set; }
	/// <summary>
	/// DbSet for the User entity.
	/// </summary>
	public DbSet<Test> Tests { get; set; }
	/// <summary>
	/// DbSet for the User entity.
	/// </summary>
	public DbSet<User> Users { get; set; }
	/// <summary>
	/// DbSet for the User entity.
	/// </summary>
	public DbSet<CodeSubmission> Submissions { get; set; }
	/// <summary>
	/// DbSet for the User entity.
	/// </summary>
	public DbSet<CodeFile> CodeFiles { get; set; }
}
