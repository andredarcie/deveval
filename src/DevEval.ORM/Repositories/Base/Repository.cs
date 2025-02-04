using DevEval.Common;
using DevEval.Common.Helpers.Pagination;
using DevEval.Common.Helpers.Sorting;
using DevEval.ORM.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DevEval.ORM.Repositories.Base
{
    public class Repository<T, TId> : IRepository<T, TId> where T : class
    {
        protected readonly DefaultContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DefaultContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "The database context cannot be null.");
            _dbSet = _context.Set<T>();
        }

        public async Task<PaginatedResult<T>> GetAllAsync(PaginationParameters parameters)
        {
            var query = _dbSet.AsQueryable();

            query = SortingHelper.ApplySorting(query, parameters.OrderBy);

            return await PaginationHelper.PaginateAsync(query, parameters.Page, parameters.PageSize);
        }

        public async Task<T?> GetByIdAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"The {typeof(T).Name} with ID {id} was not found.");
            }

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"Cannot add a null instance of {typeof(T).Name}.");
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"Cannot update a null instance of {typeof(T).Name}.");
            }

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"The {typeof(T).Name} with ID {id} was not found and cannot be deleted.");
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}