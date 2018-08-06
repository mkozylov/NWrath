using System;
using System.Threading.Tasks;

namespace NWrath.Synergy.Pipeline
{
    public class AsyncLambdaPipe<TContext>
        : AsyncPipeBase<TContext>
    {
        private Func<TContext, Func<TContext, Task>, Task> _lambdaAsync;

        public AsyncLambdaPipe(Func<TContext, Func<TContext, Task>, Task> lambdaAsync)
        {
            _lambdaAsync = lambdaAsync;
        }

        public override async Task Perform(TContext context)
        {
            await _lambdaAsync(context, (Next ?? Stub).Perform)
                    .ConfigureAwait(false);
        }

        public static implicit operator Func<TContext, Func<TContext, Task>, Task>(AsyncLambdaPipe<TContext> pipe)
        {
            return pipe._lambdaAsync;
        }
    }
}