using System;
using System.Linq.Expressions;

namespace NWrath.Synergy.Expressions
{
    public static partial class ExpressionWizardExtensions
    {
        public static ConstantExpression Null(this ExpressionWizardCharms charms)
        {
            return Expression.Constant(null, typeof(object));
        }

        public static Expression StringConcat(this ExpressionWizardCharms charms, Expression[] strings)
        {
            var body = default(Expression);

            switch (strings.Length)
            {
                case 1:
                    body = strings[0];
                    break;
                case 2:
                case 3:
                case 4:
                    var types = new Type[strings.Length];
                    var arguments = new Expression[strings.Length];

                    for (int i = 0; i < strings.Length; i++)
                    {
                        types[i] = typeof(string);
                        arguments[i] = strings[i];
                    }

                    body = Expression.Call(
                              typeof(string).GetMethod(nameof(string.Concat), types),
                              arguments
                              );
                    break;
                default:
                    body = Expression.Call(
                              typeof(string).GetMethod(nameof(string.Concat), new[] { typeof(string[]) }),
                              Expression.NewArrayInit(typeof(string), strings)
                              );
                    break;
            }

            return body;
        }
    }
}
