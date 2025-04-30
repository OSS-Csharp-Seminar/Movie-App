using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User, string>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
    }
}
