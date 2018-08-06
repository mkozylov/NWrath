using System.Linq.Expressions;

namespace NWrath.Synergy.Expressions
{
    public static class ExpressionHelper
    {
        public static readonly ConstantExpression Null = Expression.Constant(null, typeof(object));
    }
}