using System;

namespace NWrath.Synergy.Common.Structs
{
    public class FactoryUnit<TPredicate, TArgs, TInstance>
        where TPredicate : class
        where TInstance : class
    {
        public virtual string Uid { get; set; } = Guid.NewGuid().ToString();

        public virtual Func<TPredicate, bool> Predicate { get; set; }

        public virtual Func<TPredicate, TArgs, TInstance> Produce { get; set; }

        public virtual Set Store { get; set; } = new Set();

        public FactoryUnit()
        {
            Predicate = CreatePredicate;

            Produce = CreateProducer;
        }

        public FactoryUnit(
            Func<TPredicate, bool> predicate,
            Func<TPredicate, TArgs, TInstance> producer,
            string uid = null
            )
        {
            Predicate = predicate;

            Produce = producer;

            Uid = uid ?? Uid;
        }

        protected virtual bool CreatePredicate(TPredicate predicateType)
        {
            return false;
        }

        protected virtual TInstance CreateProducer(TPredicate predicateType, TArgs args)
        {
            return default(TInstance);
        }
    }
}