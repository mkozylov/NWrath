using System;

namespace NWrath.Synergy.Formatting
{
    public class LambdaStringSerializer<TObj>
        : IStringSerializer<TObj>
    {
        private Func<TObj, string> _serializeFunc;

        public LambdaStringSerializer(Func<TObj, string> serializeFunc)
        {
            _serializeFunc = serializeFunc;
        }

        public string Serialize(TObj instance)
        {
            return _serializeFunc(instance);
        }
    }
}