using System;

namespace NWrath.Synergy.Traversal
{
    public interface IObjectTraversal
    {
        object TraverseType(Type type);
    }
}