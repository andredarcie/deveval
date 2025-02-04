using DevEval.Common.Helpers.Pagination;

namespace DevEval.Common
{
    public interface IRepository<T, TId> where T : class
    {
        Task<PaginatedResult<T>> GetAllAsync(PaginationParameters parameters);
        Task<T?> GetByIdAsync(TId id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(TId id);
    }
}