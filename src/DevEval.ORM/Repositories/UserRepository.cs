using DevEval.Domain.Entities.User;
using DevEval.Domain.Repositories;
using DevEval.ORM.Contexts;
using DevEval.ORM.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DevEval.ORM.Repositories
{
    /// <summary>
    /// Implementation of IUserRepository using Entity Framework Core.
    /// </summary>
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(DefaultContext context) : base(context)
        {
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
