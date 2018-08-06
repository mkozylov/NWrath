using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NWrath.Synergy.Traversal
{
    public abstract class ObjectTraversalBase<TObjectUnit, TResult>
        : IObjectTraversal
         where TObjectUnit : ObjectUnit
    {
        public virtual TObjectUnit RootObjUnit { get; set; }

        public virtual Queue<TObjectUnit> ObjUnitsQueue { get; set; } = new Queue<TObjectUnit>();

        public virtual Func<PropertyInfo, bool> PropertyFilter { get; set; } = (p => true);

        public virtual TResult TraverseType(Type type)
        {
            Initialize();

            VisitRoot(type);

            VisitRootChilds();

            var result = PerformResult();

            return result;
        }

        public virtual TOut TraverseType<TOut>(Type type)
            where TOut : TResult
        {
            var result = TraverseType(type);

            return (TOut)result;
        }

        object IObjectTraversal.TraverseType(Type type)
        {
            return TraverseType(type);
        }

        protected virtual void Initialize()
        {
        }

        protected virtual TObjectUnit VisitRoot(Type type, bool allowAddToQueue = true)
        {
            var rootUnit = RootObjUnit ?? NewObjectUnit();

            rootUnit.ObjType = type;

            RootObjUnit = rootUnit;

            if (allowAddToQueue)
            {
                ObjUnitsQueue.Enqueue(rootUnit);
            }

            return rootUnit;
        }

        protected virtual IEnumerable<TObjectUnit> VisitRootChilds()
        {
            while (ObjUnitsQueue.Count > 0)
            {
                var currentUnit = ObjUnitsQueue.Dequeue();

                var type = currentUnit.ObjType;
                var typeInfo = type.GetTypeInfo();

                var properties = ExtractTypeProperties(type);

                properties = properties.Where(PropertyFilter);

                foreach (var propInfo in properties)
                {
                    VisitChild(currentUnit, propInfo);
                }
            }

            return RootObjUnit.SubUnits.Cast<TObjectUnit>();
        }

        protected virtual TObjectUnit VisitChild(TObjectUnit currentUnit, PropertyInfo propInfo, bool allowAddToQueue = true)
        {
            var subUnit = NewObjectUnit();

            subUnit.ObjType = propInfo.PropertyType;
            subUnit.Level = currentUnit.Level + 1;

            currentUnit.SubUnits.Add(subUnit);

            if (allowAddToQueue
                && subUnit.ObjType != typeof(string)
                && !subUnit.ObjType.GetTypeInfo().IsPrimitive
                && !subUnit.ObjType.GetTypeInfo().IsValueType)
            {
                ObjUnitsQueue.Enqueue(subUnit);
            }

            return subUnit;
        }

        protected virtual TResult PerformResult()
        {
            return default(TResult);
        }

        protected virtual IEnumerable<PropertyInfo> ExtractTypeProperties(Type type)
        {
            return type.GetTypeInfo().GetProperties();
        }

        protected virtual TObjectUnit NewObjectUnit()
        {
            return Activator.CreateInstance<TObjectUnit>();
        }
    }
}