using System;

namespace NWrath.Synergy.Common.Structs
{
    public class FactoryUnit<TArgs, TInstance>
        where TInstance : class
    {
        public virtual string Uid { get; set; } = Guid.NewGuid().ToString();

        public virtual Func<TArgs, TInstance> Produce { get; set; }

        public virtual Set Store { get; set; } = Set.Empty;

        public FactoryUnit()
        {
            Produce = CreateProducer;
        }

        public FactoryUnit(
            Func<TArgs, TInstance> producer,
            string uid = null
            )
        {
            Produce = producer;

            Uid = uid ?? Uid;
        }

        protected virtual TInstance CreateProducer(TArgs args)
        {
            return default(TInstance);
        }
    }
}