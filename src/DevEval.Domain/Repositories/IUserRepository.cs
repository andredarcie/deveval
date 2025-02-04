using DevEval.Common;
using DevEval.Domain.Entities.User;

namespace DevEval.Domain.Repositories
{
    /// <summary>
    /// Defines the operations for managing users in the repository.
    /// </summary>
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}
