using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Domain.Interfaces
{
    public interface IRepository<T, TId> where T : class
    {
        Task<T> GetByIdAsync(TId id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TId id);
    }

    public interface IRepository<T> : IRepository<T, int> where T : class
    {
    }
}
