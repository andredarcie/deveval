using DevEval.Domain.Entities.Sale;
using DevEval.Domain.Repositories;
using DevEval.ORM.Contexts;
using DevEval.ORM.Repositories.Base;

namespace DevEval.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core.
    /// </summary>
    public class SaleRepository : Repository<Sale, Guid>, ISaleRepository
    {
        public SaleRepository(DefaultContext context) : base(context)
        {
        }
    }
}