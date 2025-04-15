using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Interfaces;
using MovieApp.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId)
        {
            return await _dbContext.Reviews
                .Where(r => r.MovieId == movieId)
                .Include(r => r.User)
                .Include(r => r.Comments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.Movie)
                .ToListAsync();
        }
    }
}
