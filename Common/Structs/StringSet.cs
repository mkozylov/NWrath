using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NWrath.Synergy.Common.Extensions;
using NWrath.Synergy.Reflection.Extensions;

namespace NWrath.Synergy.Common.Structs
{
    public class StringSet
        : Dictionary<string, string>
    {
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
    }
}