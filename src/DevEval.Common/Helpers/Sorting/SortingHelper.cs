using System.Linq.Expressions;

namespace DevEval.Common.Helpers.Sorting
{
    public static class SortingHelper
    {
        public static IQueryable<T> ApplySorting<T>(
            IQueryable<T> query,
            string? orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return query;

            foreach (var order in orderBy.Split(','))
            {
                var trimmed = order.Trim();
                var descending = trimmed.EndsWith("desc", StringComparison.OrdinalIgnoreCase);
                var propertyName = trimmed.Split(' ')[0];

                var parameter = Expression.Parameter(typeof(T), "e");
                var property = Expression.Property(parameter, propertyName);
                var keySelector = Expression.Lambda(property, parameter);

                query = descending
                    ? query.Provider.CreateQuery<T>(
                        Expression.Call(
                            typeof(Queryable),
                            "OrderByDescending",
                            new Type[] { typeof(T), property.Type },
                            query.Expression,
                            Expression.Quote(keySelector)))
                    : query.Provider.CreateQuery<T>(
                        Expression.Call(
                            typeof(Queryable),
                            "OrderBy",
                            new Type[] { typeof(T), property.Type },
                            query.Expression,
                            Expression.Quote(keySelector)));
            }

            return query;
        }
    }
}
