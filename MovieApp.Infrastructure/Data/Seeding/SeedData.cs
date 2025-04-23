using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Data.Seeding
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            
            var context = services.GetRequiredService<MovieAppDbContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (context.Movies.Any())
            {
                return;
            }

            // Seed roles
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed users
            var user1 = new User
            {
                UserName = "korisnik1",
                Email = "korisnik1@example.com",
                RegisterDate = DateTime.Now.AddDays(-30),
                LastLogin = DateTime.Now.AddDays(-2),
                EmailConfirmed = true
            };

            var user2 = new User
            {
                UserName = "korisnik2",
                Email = "korisnik2@example.com",
                RegisterDate = DateTime.Now.AddDays(-25),
                LastLogin = DateTime.Now.AddDays(-1),
                EmailConfirmed = true
            };

            if (await userManager.FindByEmailAsync(user1.Email) == null)
            {
                var result = await userManager.CreateAsync(user1, "P@ssw0rd1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, "Admin");
                }
            }

            if (await userManager.FindByEmailAsync(user2.Email) == null)
            {
                var result = await userManager.CreateAsync(user2, "P@ssw0rd2");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, "User");
                }
            }

            var movie1 = new Movie
            {
                Title = "Inception",
                Genre = "Sci-Fi, Action",
                ReleaseDate = new DateTime(2010, 7, 16),
                Director = "Christopher Nolan",
                Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O."
            };

            var movie2 = new Movie
            {
                Title = "The Shawshank Redemption",
                Genre = "Drama",
                ReleaseDate = new DateTime(1994, 9, 23),
                Director = "Frank Darabont",
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."
            };

            var movie3 = new Movie
            {
                Title = "The Godfather",
                Genre = "Crime, Drama",
                ReleaseDate = new DateTime(1972, 3, 24),
                Director = "Francis Ford Coppola",
                Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son."
            };

            context.Movies.AddRange(movie1, movie2, movie3);
            await context.SaveChangesAsync();

            // Get users from database with their generated IDs
            user1 = await userManager.FindByEmailAsync("korisnik1@example.com");
            user2 = await userManager.FindByEmailAsync("korisnik2@example.com");

            var review1 = new Review
            {
                UserId = user1.Id,
                MovieId = movie1.Id,
                Rating = 9,
                Comment = "Jedan od najboljih filmova koje sam gledao. Originalni koncept i odlicna izvedba.",
                CreatedAt = DateTime.Now.AddDays(-20)
            };

            var review2 = new Review
            {
                UserId = user2.Id,
                MovieId = movie1.Id,
                Rating = 8,
                Comment = "Fantastican film s nevjerovatnim vizualnim efektima.",
                CreatedAt = DateTime.Now.AddDays(-15)
            };

            var review3 = new Review
            {
                UserId = user1.Id,
                MovieId = movie2.Id,
                Rating = 10,
                Comment = "Bezvremenski klasik. Prica o nadi i iskupljenju koja te ne ostavlja ravnodušnim.",
                CreatedAt = DateTime.Now.AddDays(-10)
            };

            context.Reviews.AddRange(review1, review2, review3);
            await context.SaveChangesAsync();

            var comment1 = new Comment
            {
                UserId = user2.Id,
                ReviewId = review1.Id,
                Content = "Slažem se! Nolan je majstor svog zanata.",
                CreatedAt = DateTime.Now.AddDays(-19)
            };

            var comment2 = new Comment
            {
                UserId = user1.Id,
                ReviewId = review2.Id,
                Content = "Slojevi snova su bili nevjerovatno dobro objašnjeni.",
                CreatedAt = DateTime.Now.AddDays(-14)
            };

            context.Comments.AddRange(comment1, comment2);
            await context.SaveChangesAsync();

            var watchlist1 = new WatchlistItem
            {
                UserId = user1.Id,
                MovieId = movie3.Id,
                AddedAt = DateTime.Now.AddDays(-5)
            };

            var watchlist2 = new WatchlistItem
            {
                UserId = user2.Id,
                MovieId = movie2.Id,
                AddedAt = DateTime.Now.AddDays(-3)
            };

            context.WatchlistItems.AddRange(watchlist1, watchlist2);
            await context.SaveChangesAsync();
        }
    }
}
