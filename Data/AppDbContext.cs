namespace PlatformService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Platform> Platforms { get; set; } = null!;

	// protected override void OnModelCreating(ModelBuilder modelBuilder)
	// {
	// 	modelBuilder.Entity<Platform>().HasData(
	// 		new Platform { Id = 1, Name = "Platform A", Publisher = "Publisher A", Cost = "Free" },
	// 		new Platform { Id = 2, Name = "Platform B", Publisher = "Publisher B", Cost = "Paid" },
	// 		new Platform { Id = 3, Name = "Platform C", Publisher = "Publisher B", Cost = "Paid-as-you-go" }					
	// 	);
	// }		
}
