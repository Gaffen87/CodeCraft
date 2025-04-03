using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
		modelBuilder.Entity<Domain.Entities.Group>().HasMany(e => e.Exercises).WithMany(g => g.Groups)
			.UsingEntity<GroupExercise>();
	}

	public DbSet<Exercise> Exercises { get; set; }
	public DbSet<Domain.Entities.Group> Groups { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Session> Sessions { get; set; }
	public DbSet<Test> Tests { get; set; }
	public DbSet<User> Users { get; set; }
}
