using NWrath.Synergy.Common.Extensions;
using NWrath.Synergy.Common.Extensions.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NWrath.Synergy.Pipeline
{
    public static class PipelineWizard
    {
        public static IAsyncPipe<TContext> Create<TContext>(params Func<TContext, Func<TContext, Task>, Task>[] pipes)
        {
            var resultPipes = pipes.Select(x => new AsyncLambdaPipe<TContext>(x))
                                   .ToArray();

            return Create(resultPipes);
        }

        public static IPipe<TContext> Create<TContext>(params Action<TContext, Action<TContext>>[] pipes)
        {
            var resultPipes = pipes.Select(x => new LambdaPipe<TContext>(x))
                                   .ToArray();

            return Create(resultPipes);
        }

        public static IPipe<TContext> Create<TContext>(params IPipe<TContext>[] pipes)
        {
            if (pipes.IsEmpty())
            {
                return null;
            }

            for (int i = 0; i < pipes.Length; i++)
            {
                var next = pipes.ElementAtOrDefault(i + 1);

                var cur = pipes[i];

                if (next != null)
                {
                    if (cur.Next == null)
                    {
                        cur.Next = next;
                    }
                    else
                    {
                        var lp = cur;
                        var np = cur.Next;

                        while (np != null && np != PipeBase<TContext>.Stub)
                        {
                            lp = np;
                            np = np.Next;
                        }

                        lp.Next = next;
                    }
                }
            }

            return pipes[0];
        }

        public static IAsyncPipe<TContext> Create<TContext>(params IAsyncPipe<TContext>[] pipes)
        {
            if (pipes.IsEmpty())
            {
                return null;
            }

            for (int i = 0; i < pipes.Length; i++)
            {
                var next = pipes.ElementAtOrDefault(i + 1);

                var cur = pipes[i];

                if (next != null)
                {
                    if (cur.Next == null)
                    {
                        cur.Next = next;
                    }
                    else
                    {
                        var lp = cur;
                        var np = cur.Next;

                        while (np != null
                            && np != AsyncPipeBase<TContext>.Stub)
                        {
                            lp = np;
                            np = np.Next;
                        }

                        lp.Next = next;
                    }
                }
            }

            return pipes[0];
        }
    }
}