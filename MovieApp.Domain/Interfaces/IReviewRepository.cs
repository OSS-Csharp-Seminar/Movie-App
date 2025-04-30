using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId);
    }
}
