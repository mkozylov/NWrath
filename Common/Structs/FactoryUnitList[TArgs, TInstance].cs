using System;
using System.Collections.Generic;
using System.Linq;

namespace NWrath.Synergy.Common.Structs
{
    public class FactoryUnitList<TArgs, TInstance>
        : List<FactoryUnit<TArgs, TInstance>>
        where TInstance : class
    {
        public FactoryUnitList()
        {
        }

        public FactoryUnitList(IEnumerable<FactoryUnit<TArgs, TInstance>> collection)
        {
            AddRange(collection);
        }

        public FactoryUnitList<TArgs, TInstance> Add(
            Func<TArgs, TInstance> producer,
            string uid = null
            )
        {
            var unit = new FactoryUnit<TArgs, TInstance>(producer, uid);

            Add(unit);

            return this;
        }

        public virtual TFactory Get<TFactory>()
            where TFactory : FactoryUnit<TArgs, TInstance>
        {
            var type = typeof(TFactory);

            return (TFactory)this.FirstOrDefault(x => x.GetType() == type);
        }

        public virtual void Remove<TFactory>()
            where TFactory : FactoryUnit<TArgs, TInstance>
        {
            var factory = Get<TFactory>();

            if (factory != null)
            {
                Remove(factory);
            }
        }

        public virtual void Replace<TFactory>(TFactory newFactory)
            where TFactory : FactoryUnit<TArgs, TInstance>
        {
            Remove<TFactory>();

            Add(newFactory);
        }

        public virtual FactoryUnit<TArgs, TInstance> Get(string uid)
        {
            return this.FirstOrDefault(x => x.Uid == uid);
        }

        public virtual void Remove(string uid)
        {
            var factory = Get(uid);

            if (factory != null)
            {
                Remove(factory);
            }
        }

        public virtual void Replace(FactoryUnit<TArgs, TInstance> newUnit)
        {
            Remove(newUnit.Uid);

            Add(newUnit);
        }
    }
}