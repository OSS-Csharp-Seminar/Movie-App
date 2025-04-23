using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Domain.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetByGenreAsync(string genre);
        Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm);
    }
}
