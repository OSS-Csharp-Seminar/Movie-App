using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Application.Interfaces
{
    public interface IReviewService
    {
        Task<Review> GetReviewByIdAsync(int id);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> CreateReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(string userId);
        Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(int movieId);
    }
}
