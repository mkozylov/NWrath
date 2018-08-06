using System;
using System.Collections.Generic;
using System.Linq;

namespace NWrath.Synergy.Common.Structs
{
    public class FactoryUnitList<TPredicate, TArgs, TInstance>
        : List<FactoryUnit<TPredicate, TArgs, TInstance>>
        where TPredicate : class
        where TInstance : class
    {
        public FactoryUnitList()
        {
        }

        public FactoryUnitList(IEnumerable<FactoryUnit<TPredicate, TArgs, TInstance>> collection)
        {
            AddRange(collection);
        }

        public FactoryUnitList<TPredicate, TArgs, TInstance> Add(
            Func<TPredicate, bool> predicate,
            Func<TPredicate, TArgs, TInstance> producer,
            string uid = null
            )
        {
            var unit = new FactoryUnit<TPredicate, TArgs, TInstance>(predicate, producer, uid);

            Add(unit);

            return this;
        }

        public virtual TFactory Get<TFactory>()
            where TFactory : FactoryUnit<TPredicate, TArgs, TInstance>
        {
            var type = typeof(TFactory);

            return (TFactory)this.FirstOrDefault(x => x.GetType() == type);
        }

        public virtual void Remove<TFactory>()
            where TFactory : FactoryUnit<TPredicate, TArgs, TInstance>
        {
            var factory = Get<TFactory>();

            if (factory != null)
            {
                Remove(factory);
            }
        }

        public virtual void Replace<TFactory>(TFactory newFactory)
            where TFactory : FactoryUnit<TPredicate, TArgs, TInstance>
        {
            Remove<TFactory>();

            Add(newFactory);
        }

        public virtual FactoryUnit<TPredicate, TArgs, TInstance> Get(string uid)
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

        public virtual void Replace(FactoryUnit<TPredicate, TArgs, TInstance> newUnit)
        {
            Remove(newUnit.Uid);

            Add(newUnit);
        }
    }
}