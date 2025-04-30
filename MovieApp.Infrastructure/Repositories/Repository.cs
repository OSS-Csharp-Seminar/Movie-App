using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Interfaces;
using MovieApp.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Repositories
{
    public class Repository<T, TId> : IRepository<T, TId> where T : class
    {
        protected readonly MovieAppDbContext _dbContext;

        public Repository(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    public class Repository<T> : Repository<T, int>, IRepository<T> where T : class
    {
        public Repository(MovieAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
