using System;
using System.Collections.Generic;

namespace NWrath.Synergy.Formatting
{
    public interface IStringFormatStore<TObj>
        : IDictionary<string, Func<TObj, string>>
    {

    }
}