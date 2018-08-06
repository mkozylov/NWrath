using System.Threading.Tasks;

namespace NWrath.Synergy.Pipeline
{
    public abstract class AsyncPipeBase<TContext>
        : IAsyncPipe<TContext>
    {
        public static IAsyncPipe<TContext> Stub { get; } = new StubPipe<TContext>();

        public IAsyncPipe<TContext> Next { get => _next; set { _next = value ?? Stub; } }

        private IAsyncPipe<TContext> _next = Stub;

        public abstract Task Perform(TContext context);

        protected virtual async Task PerformNext(TContext context)
        {
            await _next.Perform(context)
                       .ConfigureAwait(false);
        }

        private class StubPipe<T>
            : IAsyncPipe<TContext>
        {
            public IAsyncPipe<TContext> Next { get; set; }

            public Task Perform(TContext context)
            {
                return Task.CompletedTask;
            }
        }
    }
}