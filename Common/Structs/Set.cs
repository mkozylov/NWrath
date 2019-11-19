using System;
using System.Collections.Generic;

namespace NWrath.Synergy.Common.Structs
{
    public class Set
        : Dictionary<string, object>
    {
        public Set()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public Set(IDictionary<string, object> dictionary)
            : base(dictionary, StringComparer.OrdinalIgnoreCase)
        {
        }

        public Set(IEqualityComparer<string> comparer)
         : base(comparer)
        {
        }

        public Set(IDictionary<string, object> dictionary, IEqualityComparer<string> comparer)
          : base(dictionary, comparer)
        {
        }
    }
}