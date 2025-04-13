using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM.Query
{
    /// <summary>
    /// Provides functionality to filter and sort an IQueryable collection of entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity being queried.</typeparam>
    public sealed class QueryFilterAndSorter<TEntity>
    {
        /// <summary>
        /// The queryable collection of entities to be filtered and sorted.
        /// </summary>
        private readonly IQueryable<TEntity> _query;

        /// <summary>
        /// The string specifying the ordering criteria.
        /// </summary>
        private readonly string _orderBy;

        /// <summary>
        /// A dictionary containing filter criteria where the key is the property name and the value is the filter value.
        /// </summary>
        private readonly Dictionary<string, string>? _filters;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFilterAndSorter{TEntity}"/> class.
        /// </summary>
        /// <param name="query">The queryable collection of entities.</param>
        /// <param name="orderBy">The ordering criteria as a string.</param>
        /// <param name="filters">The filter criteria as a dictionary.</param>
        public QueryFilterAndSorter(IQueryable<TEntity> query, string orderBy, Dictionary<string, string>? filters)
        {
            _query = query;
            _orderBy = orderBy;
            _filters = filters;
        }

        /// <summary>
        /// Applies the filters and sorting to the queryable collection.
        /// </summary>
        /// <returns>The filtered and sorted queryable collection.</returns>
        public IQueryable<TEntity> Apply()
        {
            var query = _query;

            if (_filters != null)
            {
                query = ApplyFilters(query, _filters);
            }

            if (!string.IsNullOrEmpty(_orderBy))
            {
                query = ApplyOrdering(query, _orderBy);
            }

            return query;
        }

        /// <summary>
        /// Applies the specified filters to the queryable collection.
        /// </summary>
        /// <param name="query">The queryable collection to filter.</param>
        /// <param name="filters">The filter criteria as a dictionary.</param>
        /// <returns>The filtered queryable collection.</returns>
        private static IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query, Dictionary<string, string> filters)
        {
            foreach (var filter in filters)
            {
                if (filter.Key.StartsWith("_min"))
                {
                    query = ApplyMinFilter(query, filter);
                    continue;
                }

                if (filter.Key.StartsWith("_max"))
                {
                    query = ApplyMaxFilter(query, filter);
                    continue;
                }

                if (filter.Value.Contains('*'))
                {
                    query = ApplyContainsFilter(query, filter);
                    continue;
                }

                query = ApplyEqualityFilter(query, filter);
            }

            return query;
        }

        /// <summary>
        /// Applies a minimum value filter to the queryable collection.
        /// </summary>
        /// <param name="query">The queryable collection to filter.</param>
        /// <param name="filter">The filter criteria as a key-value pair.</param>
        /// <returns>The filtered queryable collection.</returns>
        private static IQueryable<TEntity> ApplyMinFilter(IQueryable<TEntity> query, KeyValuePair<string, string> filter)
        {
            var property = filter.Key.Substring(4);
            if (!PropertyExists(property)) return query;

            return query.Where(BuildPredicate<TEntity>(property, filter.Value, ExpressionType.GreaterThanOrEqual));
        }

        /// <summary>
        /// Applies a maximum value filter to the queryable collection.
        /// </summary>
        /// <param name="query">The queryable collection to filter.</param>
        /// <param name="filter">The filter criteria as a key-value pair.</param>
        /// <returns>The filtered queryable collection.</returns>
        private static IQueryable<TEntity> ApplyMaxFilter(IQueryable<TEntity> query, KeyValuePair<string, string> filter)
        {
            var property = filter.Key.Substring(4);
            if (!PropertyExists(property)) return query;

            return query.Where(BuildPredicate<TEntity>(property, filter.Value, ExpressionType.LessThanOrEqual));
        }

        /// <summary>
        /// Applies a "contains" filter to the queryable collection.
        /// </summary>
        /// <param name="query">The queryable collection to filter.</param>
        /// <param name="filter">The filter criteria as a key-value pair.</param>
        /// <returns>The filtered queryable collection.</returns>
        private static IQueryable<TEntity> ApplyContainsFilter(IQueryable<TEntity> query, KeyValuePair<string, string> filter)
        {
            var property = filter.Key;
            var value = filter.Value.Replace("*", "");
            if (!PropertyExists(property)) return query;

            return query.Where(BuildContainsPredicate<TEntity>(property, value));
        }

        /// <summary>
        /// Applies an equality filter to the queryable collection.
        /// </summary>
        /// <param name="query">The queryable collection to filter.</param>
        /// <param name="filter">The filter criteria as a key-value pair.</param>
        /// <returns>The filtered queryable collection.</returns>
        private static IQueryable<TEntity> ApplyEqualityFilter(IQueryable<TEntity> query, KeyValuePair<string, string> filter)
        {
            if (!PropertyExists(filter.Key)) return query;

            return query.Where(BuildPredicate<TEntity>(filter.Key, filter.Value, ExpressionType.Equal));
        }

        /// <summary>
        /// Applies ordering to the queryable collection based on the specified criteria.
        /// </summary>
        /// <param name="query">The queryable collection to order.</param>
        /// <param name="orderBy">The ordering criteria as a string.</param>
        /// <returns>The ordered queryable collection.</returns>
        private static IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, string orderBy)
        {
            var orderParams = orderBy.Replace("\"", "").Split(',');

            foreach (var param in orderParams)
            {
                var trimmedParam = param.Trim();
                var isDescending = trimmedParam.EndsWith(" desc");
                var property = isDescending
                    ? trimmedParam.Replace(" desc", "")
                    : trimmedParam.Replace(" asc", "");

                if (!PropertyExists(property))
                    continue;

                query = isDescending
                    ? query.OrderByDescending(BuildSelector<TEntity>(property))
                    : query.OrderBy(BuildSelector<TEntity>(property));
            }

            return query;
        }

        /// <summary>
        /// Checks if a property exists on the entity type.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>True if the property exists; otherwise, false.</returns>
        private static bool PropertyExists(string propertyName)
        {
            return typeof(TEntity).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase) != null;
        }

        /// <summary>
        /// Builds a predicate expression for filtering based on a property, value, and comparison type.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="propertyName">The name of the property to filter on.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="comparisonType">The type of comparison to perform.</param>
        /// <returns>A predicate expression for filtering.</returns>
        private static Expression<Func<TEntity, bool>> BuildPredicate<T>(string propertyName, string value, ExpressionType comparisonType)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, propertyName);

            var propertyType = property.Type;
            object convertedValue;

            if (propertyType.IsEnum)
            {
                convertedValue = Enum.Parse(propertyType, value, ignoreCase: true);
            }
            else
            {
                convertedValue = Convert.ChangeType(value, propertyType, CultureInfo.InvariantCulture);
            }

            var constant = Expression.Constant(convertedValue, propertyType);
            var comparison = Expression.MakeBinary(comparisonType, property, constant);
            return Expression.Lambda<Func<TEntity, bool>>(comparison, parameter);
        }

        /// <summary>
        /// Builds a "contains" predicate expression for filtering based on a property and value.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="propertyName">The name of the property to filter on.</param>
        /// <param name="value">The value to check for containment.</param>
        /// <returns>A predicate expression for filtering.</returns>
        private static Expression<Func<TEntity, bool>> BuildContainsPredicate<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsCall = Expression.Call(property, containsMethod, constant);
            return Expression.Lambda<Func<TEntity, bool>>(containsCall, parameter);
        }

        /// <summary>
        /// Builds a selector expression for ordering based on a property.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="propertyName">The name of the property to order by.</param>
        /// <returns>A selector expression for ordering.</returns>
        private static Expression<Func<TEntity, object>> BuildSelector<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, propertyName);
            var convert = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TEntity, object>>(convert, parameter);
        }
    }
}
