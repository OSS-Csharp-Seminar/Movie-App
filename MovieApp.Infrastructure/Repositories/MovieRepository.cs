using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Interfaces;
using MovieApp.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetByGenreAsync(string genre)
        {
            return await _dbContext.Movies
                .Where(m => m.Genre.Contains(genre))
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm)
        {
            return await _dbContext.Movies
                .Where(m => m.Title.Contains(searchTerm) || 
                           m.Director.Contains(searchTerm) || 
                           m.Description.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
