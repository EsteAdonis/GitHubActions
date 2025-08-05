
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt =>
		opt.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
if (context.Database.EnsureCreated())
{
	// context.SeedData(); // Seed initial data
	if (!context.Platforms.Any())
	{
		context.Platforms.AddRange(
			new Platform { Id = 1, Name = "Platform A", Publisher = "Publisher A", Cost = "Free" },
			new Platform { Id = 2, Name = "Platform B", Publisher = "Publisher B", Cost = "Paid" },
			new Platform { Id = 3, Name = "Platform C", Publisher = "Publisher B", Cost = "Paid-as-you-go" }
		);
		context.SaveChanges(); // Save changes to the database		
	}
}


app.Run();
