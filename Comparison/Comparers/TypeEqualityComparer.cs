using System;
using System.Collections.Generic;
using System.Text;

namespace NWrath.Synergy.Comparison.Comparers
{
    public class TypeEqualityComparer : IEqualityComparer<Type>
    {
        public bool Equals(Type x, Type y)
        {
            return x == y;
        }

        public int GetHashCode(Type obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}
