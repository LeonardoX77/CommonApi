using Common.Core.Generic.DynamicQueryFilter.Interfaces;
using System.Linq.Expressions;

namespace Common.Core.Generic.DynamicQueryFilter.DynamicExpressions
{
    /// <summary>
    /// Filter for a range of values applicable to any entity.
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <typeparam name="TQueryFilter">DTO Entity</typeparam>
    public class RangeDynamicExpression<T, TQueryFilter> : DynamicExpression<T, TQueryFilter>
        where T : class, new()
    where TQueryFilter : class, IDynamicQueryFilter, new()
    {
        private readonly object _min;
        private readonly object _max;
        private readonly object _equal;
        private readonly string _propertyName;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="propertyName">The name of the property to filter.</param>
        /// <param name="min">Minimum value for the range filter.</param>
        /// <param name="max">Maximum value for the range filter.</param>
        /// <param name="equal">Specific value for equality filter.</param>
        public RangeDynamicExpression(string propertyName, object min = null, object max = null, object equal = null)
        {
            _propertyName = propertyName;
            _min = min;
            _max = max;
            _equal = equal;
            _predicate = SetPredicate();
        }

        /// <summary>
        /// Sets the predicate for the range filter.
        /// </summary>
        /// <returns>Expression representing the predicate.</returns>
        protected override Expression<Func<T, bool>> SetPredicate()
        {
            var parameter = Expression.Parameter(typeof(T), "entity");
            var propertyExpression = Expression.PropertyOrField(parameter, _propertyName);
            Expression finalExpression = Expression.Constant(true, typeof(bool));

            // Apply minimum value comparison if specified
            finalExpression = GetMinComparison(propertyExpression, finalExpression);

            // Apply maximum value comparison if specified
            finalExpression = GetMaxComparison(propertyExpression, finalExpression);

            // Apply equality comparison if specified
            finalExpression = GetEqualComparison(propertyExpression, finalExpression);

            return Expression.Lambda<Func<T, bool>>(finalExpression, parameter);
        }

        /// <summary>
        /// Creates an equality comparison expression.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="finalExpression">The final expression to combine with.</param>
        /// <returns>Combined expression with equality comparison.</returns>
        private Expression GetEqualComparison(MemberExpression propertyExpression, Expression finalExpression)
        {
            if (_equal != null)
            {
                Expression equalExpression = Expression.Equal(
                    Expression.Convert(propertyExpression, _equal.GetType()),
                    Expression.Constant(_equal, _equal.GetType())
                );
                finalExpression = Expression.AndAlso(finalExpression, equalExpression);
            }

            return finalExpression;
        }

        /// <summary>
        /// Creates a maximum value comparison expression.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="finalExpression">The final expression to combine with.</param>
        /// <returns>Combined expression with maximum value comparison.</returns>
        private Expression GetMaxComparison(MemberExpression propertyExpression, Expression finalExpression)
        {
            if (_max != null)
            {
                Expression maxExpression = Expression.LessThanOrEqual(
                    Expression.Convert(propertyExpression, _max.GetType()),
                    Expression.Constant(_max, _max.GetType())
                );
                finalExpression = Expression.AndAlso(finalExpression, maxExpression);
            }

            return finalExpression;
        }

        /// <summary>
        /// Creates a minimum value comparison expression.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="finalExpression">The final expression to combine with.</param>
        /// <returns>Combined expression with minimum value comparison.</returns>
        private Expression GetMinComparison(MemberExpression propertyExpression, Expression finalExpression)
        {
            if (_min != null)
            {
                Expression minExpression = Expression.GreaterThanOrEqual(
                    Expression.Convert(propertyExpression, _min.GetType()),
                    Expression.Constant(_min, _min.GetType())
                );
                finalExpression = Expression.AndAlso(finalExpression, minExpression);
            }

            return finalExpression;
        }
    }
}
