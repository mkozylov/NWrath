using NWrath.Synergy.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NWrath.Synergy.Formatting
{
    public class StringFormatStore<TObj>
        : Dictionary<string, Func<TObj, string>>, IStringFormatStore<TObj>
    {
        public StringFormatStore()
        {
            InitFormats();
        }

        new public virtual Func<TObj, string> this[string key]
        {
            get => (ContainsKey(key) ? base[key] : null);
            set => base[key] = value;
        }

        #region Internal

        private void InitFormats()
        {
            this["nl"] = m => Environment.NewLine;
        } 

        #endregion
    }
}