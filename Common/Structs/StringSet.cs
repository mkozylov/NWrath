using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NWrath.Synergy.Reflection.Extensions;

namespace NWrath.Synergy.Common.Structs
{
    public class StringSet
        : Dictionary<string, string>
    {
        public static StringSet Empty { get { return new StringSet(StringComparer.OrdinalIgnoreCase); } }

        private static MethodInfo _fromObjectMI;

        static StringSet()
        {
            _fromObjectMI = typeof(StringSet).GetStaticGenericMethod(nameof(FromObject), 1, 1);
        }

        public StringSet()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public StringSet(IDictionary<string, string> dictionary)
            : base(dictionary, StringComparer.OrdinalIgnoreCase)
        {
        }

        public StringSet(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        public StringSet(IDictionary<string, string> dictionary, IEqualityComparer<string> comparer)
          : base(dictionary, comparer)
        {
        }

        public virtual string AsJson()
        {
            return $"{{ {string.Join(", ", this.Select(x => $"\"{x.Key}\":\"{x.Value}\""))} }}";
        }

        public static StringSet FromObject(object data)
        {
            return (StringSet)_fromObjectMI.MakeGenericMethod(data.GetType())
                                           .Invoke(null, new[] { data });
        }

        public static StringSet FromObject<T>(T data)
        {
            var dictionary = Cache<T>.GetPropertiesStringValues(data);

            return new StringSet(dictionary);
        }

        private static class Cache<T>
        {
            private static PropertyInfo[] _members;

            static Cache()
            {
                _members = typeof(T).GetProperties();
            }

            public static Dictionary<string, string> GetPropertiesStringValues(T instance)
            {
                return _members.ToDictionary(
                    k => k.Name,
                    v => v.GetValue(instance) + ""
                    );
            }
        }
    }
}