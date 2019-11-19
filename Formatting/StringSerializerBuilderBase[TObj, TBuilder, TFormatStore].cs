using FastExpressionCompiler;
using NWrath.Synergy.Common.Extensions;
using NWrath.Synergy.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NWrath.Synergy.Formatting
{
    public abstract class StringSerializerBuilderBase<TObj, TBuilder, TFormatStore, TSerializer>
        where TBuilder : class
        where TFormatStore : class, IDictionary<string, Func<TObj, string>>, new()
        where TSerializer: class, IStringSerializer<TObj>
    {
        public virtual string OutputTemplate { get; set; }

        public virtual TFormatStore Formats { get; set; } = new TFormatStore();

        public virtual Func<PropertyInfo, bool> PropertyFilter { get; set; }

        protected static PropertyInfo[] props = typeof(TObj).GetProperties();
        protected ITokenParser parser = new TokenParser();

        public virtual TBuilder UseFormats(Action<TFormatStore> formatsApply)
        {
            Formats = new TFormatStore();

            formatsApply(Formats);

            return this as TBuilder;
        }

        public virtual TBuilder UseFormats(TFormatStore formats)
        {
            Formats = formats;

            return this as TBuilder;
        }

        public virtual TBuilder UseOutputTemplate(string outputTemplate)
        {
            OutputTemplate = outputTemplate;

            return this as TBuilder;
        }

        public virtual TBuilder UsePropertyFilter(Func<PropertyInfo, bool> filter)
        {
            PropertyFilter = filter;

            return this as TBuilder;
        }

        public virtual Func<TObj, string> BuildLambda()
        {
            Formats = Formats ?? new TFormatStore();
            PropertyFilter = PropertyFilter ?? (p => true);

            var instanceExpr = Expression.Parameter(typeof(TObj), "instance");
            var strExprs = new List<Expression>();

            if (OutputTemplate.IsEmpty())
            {
                strExprs.Add(
                    Expression.Call(instanceExpr, nameof(ToString), null)
                    );
            }
            else
            {
                var filteredProps = props.Where(PropertyFilter)
                                          .ToArray();

                var tokens = parser.Parse(OutputTemplate);

                foreach (var t in tokens)
                {
                    var valExpr = default(Expression);
                    var propertyInfo = default(PropertyInfo);

                    if (t.IsLiteral)
                    {
                        valExpr = Expression.Constant(t.Value);
                    }
                    else
                    {
                        if (Formats.ContainsKey(t.Value))
                        {
                            var callFormatExpr = Expression.Constant(Formats[t.Value]);

                            valExpr = Expression.Invoke(callFormatExpr, instanceExpr);
                        }
                        else if ((propertyInfo = filteredProps.FirstOrDefault(x => x.Name.Equals(t.Value, StringComparison.OrdinalIgnoreCase))) != null)
                        {
                            var propertyExpr = Expression.Property(instanceExpr, propertyInfo);

                            valExpr = propertyInfo.PropertyType == typeof(string)
                                ? (Expression)propertyExpr
                                : Expression.Call(propertyExpr, nameof(ToString), null);
                        }
                        else
                        {
                            valExpr = Expression.Constant(t.Key);
                        }
                    }

                    strExprs.Add(valExpr);
                }
            }

            var body = ExpressionWizard.Spell.StringConcat(strExprs.ToArray());
            var lambda = Expression.Lambda<Func<TObj, string>>(body, instanceExpr);
            var serializerFunc = lambda.CompileFast();

            return serializerFunc;
        }

        public abstract TSerializer BuildSerializer();
    }
}
