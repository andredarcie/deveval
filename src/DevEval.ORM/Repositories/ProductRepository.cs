using DevEval.Common.Helpers.Pagination;
using DevEval.Common.Helpers.Sorting;
using DevEval.Domain.Entities.Product;
using DevEval.Domain.Repositories;
using DevEval.ORM.Contexts;
using DevEval.ORM.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DevEval.ORM.Repositories
{
    /// <summary>
    /// Implementation of IProductRepository using Entity Framework Core.
    /// </summary>
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(DefaultContext context) : base(context)
        {
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _context.Products
                                 .Select(p => p.Category)
                                 .Distinct()
                                 .ToListAsync();
        }

        public async Task<PaginatedResult<Product>> GetProductsByCategoryAsync(string category, PaginationParameters parameters)
        {
            var query = _context.Products.Where(p => p.Category == category);

            query = SortingHelper.ApplySorting(query, parameters.OrderBy);

            return await PaginationHelper.PaginateAsync(query, parameters.Page, parameters.PageSize);
        }
    }
}