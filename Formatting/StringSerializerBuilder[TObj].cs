using FastExpressionCompiler;
using NWrath.Synergy.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NWrath.Synergy.Formatting
{
    public class StringSerializerBuilder<TObj>
        : StringSerializerBuilderBase<TObj, StringSerializerBuilder<TObj>, StringFormatStore<TObj>, IStringSerializer<TObj>>
    {
        public override IStringSerializer<TObj> BuildSerializer()
        {
            var serializerFunc = BuildLambda();

            return new LambdaStringSerializer<TObj>(serializerFunc);
        }
    }
}