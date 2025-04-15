using Microsoft.EntityFrameworkCore;
using MovieApp.Application.Interfaces;
using MovieApp.Application.Services;
using MovieApp.Domain.Interfaces;
using MovieApp.Infrastructure.Data;
using MovieApp.Infrastructure.Data.Seeding;
using MovieApp.Infrastructure.Repositories;
using MovieApp.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Set up console logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

Console.WriteLine("Starting application...");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure database
Console.WriteLine("Configuring database...");
builder.Services.AddDbContext<MovieAppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

Console.WriteLine("Building application...");
var app = builder.Build();

// Apply migrations and seed the database in background
Task.Run(async () => {
    try {
        Console.WriteLine("Applying database migrations...");
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<MovieAppDbContext>();
            
            // Primijenite migracije
            await context.Database.MigrateAsync();
            Console.WriteLine("Database migrations applied successfully!");
            
            // Seed baze podataka
            Console.WriteLine("Seeding database...");
            await SeedData.Initialize(services);
            Console.WriteLine("Database seeding completed!");
        }
    }
    catch (Exception ex) {
        Console.WriteLine($"Error with database initialization: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
    }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
