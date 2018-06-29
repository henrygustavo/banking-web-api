namespace Banking.Infrastructure.Repository.Common
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderBy, string orderDirection)
        {
            var expression = source.Expression;

            var parameter = Expression.Parameter(typeof(T), "x");
            var selector = Expression.PropertyOrField(parameter, orderBy);
            var method = string.Equals(orderDirection, "desc", StringComparison.OrdinalIgnoreCase)
                ? "OrderByDescending"
                : "OrderBy";
            expression = Expression.Call(typeof(Queryable), method,
                new[] { source.ElementType, selector.Type },
                expression, Expression.Quote(Expression.Lambda(selector, parameter)));

            return source.Provider.CreateQuery<T>(expression);
        }
    }
}
