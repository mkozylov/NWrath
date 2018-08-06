using System;
using System.Collections.Generic;
using System.Linq;

namespace NWrath.Synergy.Common.Structs
{
    public class StringSet
        : Dictionary<string, string>
    {
        public static StringSet Empty { get { return new StringSet(StringComparer.OrdinalIgnoreCase); } }

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
            var type = data.GetType();

            var dictionary = type.GetProperties()
                                 .ToDictionary(
                                     k => k.Name,
                                     v => v.GetValue(data) + ""
                                     );

            return new StringSet(dictionary);
        }
    }
}