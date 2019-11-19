using System.Linq.Expressions;

namespace NWrath.Synergy.Expressions.Extensions
{
    public static partial class CommonExpressionExtensions
    {
        public static Expression AndCoalesce(this Expression left, Expression right)
        {
            return left == null ? right : Expression.AndAlso(left, right);
        }

        public static Expression OrCoalesce(this Expression left, Expression right)
        {
            return left == null ? right : Expression.OrElse(left, right);
        }

        public static MemberExpression GetMember<TSelector>(this Expression<TSelector> propExpr)
        {
            var unaryExpr = propExpr.Body as UnaryExpression;

            return (MemberExpression)(unaryExpr == null ? propExpr.Body : unaryExpr.Operand);
        }
    }
}