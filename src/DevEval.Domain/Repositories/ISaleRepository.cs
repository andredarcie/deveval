using DevEval.Common;
using DevEval.Domain.Entities.Sale;

namespace DevEval.Domain.Repositories
{
    /// <summary>
    /// Defines the operations for managing sales in the repository.
    /// </summary>
    public interface ISaleRepository : IRepository<Sale, Guid>
    {
    }
}
