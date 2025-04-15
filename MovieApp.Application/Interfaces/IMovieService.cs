using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Application.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> CreateMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
        Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm);
    }
}
