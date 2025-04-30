using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MovieApp.Infrastructure.Data
{
    public class MovieAppDbContextFactory : IDesignTimeDbContextFactory<MovieAppDbContext>
    {
        public MovieAppDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "MovieApp.Web"));
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<MovieAppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            builder.UseSqlite(connectionString);
            
            return new MovieAppDbContext(builder.Options);
        }
    }
}
