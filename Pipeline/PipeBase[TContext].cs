namespace NWrath.Synergy.Pipeline
{
    public abstract class PipeBase<TContext>
        : IPipe<TContext>
    {
        public static IPipe<TContext> Stub { get; } = new StubPipe<TContext>();

        public IPipe<TContext> Next { get => _next; set { _next = value ?? Stub; } }

        private IPipe<TContext> _next = Stub;

        public abstract void Perform(TContext context);

        protected virtual void PerformNext(TContext context)
        {
            _next.Perform(context);
        }

        private class StubPipe<T>
            : IPipe<TContext>
        {
            public IPipe<TContext> Next { get; set; }

            public void Perform(TContext context)
            {
            }
        }
    }
}