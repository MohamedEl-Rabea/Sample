using System;
using System.Linq.Expressions;

namespace Moj.CMS.Shared.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            var parameter = Expression.Parameter(typeof(T));

            var result = GetNewExpressions(leftExpression, rightExpression, parameter);

            var andExp = Expression.And(result.left, result.right);

            return Expression.Lambda<Func<T, bool>>(andExp, parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            var parameter = Expression.Parameter(typeof(T));

            var result = GetNewExpressions(leftExpression, rightExpression, parameter);

            var orExp = Expression.Or(result.left, result.right);

            return Expression.Lambda<Func<T, bool>>(orExp, parameter);
        }

        private static (Expression left, Expression right) GetNewExpressions<T>(Expression<Func<T, bool>> leftExpression,
                                                                                Expression<Func<T, bool>> rightExpression,
                                                                                ParameterExpression parameter)
        {
            var left = ReplaceExpressionParameters(leftExpression, parameter);

            var right = ReplaceExpressionParameters(rightExpression, parameter);

            return (left, right);
        }

        private static Expression ReplaceExpressionParameters<T>(Expression<Func<T, bool>> exp, ParameterExpression parameter)
        {

            var expVisitor = new ReplaceExpressionVisitor(exp.Parameters[0], parameter);

            return expVisitor.Visit(exp.Body);
        }
    }

    public class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}
