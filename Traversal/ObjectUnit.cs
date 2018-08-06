using NWrath.Synergy.Common.Structs;
using System;
using System.Collections.Generic;

namespace NWrath.Synergy.Traversal
{
    public class ObjectUnit
    {
        public Type ObjType { get; set; }

        public int Level { get; set; }

        public List<ObjectUnit> SubUnits { get; set; } = new List<ObjectUnit>();

        public Set Properties { get; set; } = new Set(StringComparer.OrdinalIgnoreCase);

        public ObjectUnit()
        {
        }

        public ObjectUnit(
            Type objectType,
            int level
            )
        {
            ObjType = objectType;
            Level = level;
        }
    }
}