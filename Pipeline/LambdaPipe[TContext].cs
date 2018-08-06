using System;

namespace NWrath.Synergy.Pipeline
{
    public class LambdaPipe<TContext>
        : PipeBase<TContext>
    {
        private Action<TContext, Action<TContext>> _lambda;

        public LambdaPipe(Action<TContext, Action<TContext>> lambda)
        {
            _lambda = lambda;
        }

        public override void Perform(TContext context)
        {
            _lambda(context, (Next ?? Stub).Perform);
        }

        public static implicit operator Action<TContext, Action<TContext>>(LambdaPipe<TContext> pipe)
        {
            return pipe._lambda;
        }
    }
}