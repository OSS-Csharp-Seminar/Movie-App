using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Interfaces;
using MovieApp.Infrastructure.Data;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, string>, IUserRepository
    {
        public UserRepository(MovieAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
