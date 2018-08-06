using NWrath.Synergy.Common.Extensions.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWrath.Synergy.Pipeline
{
    public class AsyncPipeCollection<TContext>
        : IList<IAsyncPipe<TContext>>
        where TContext : class
    {
        public IAsyncPipe<TContext> Pipeline { get => _pipeline.Value; }

        public int Count => _pipes.Count;

        public bool IsReadOnly { get; } = false;

        private List<IAsyncPipe<TContext>> _pipes = new List<IAsyncPipe<TContext>>();
        private Lazy<IAsyncPipe<TContext>> _pipeline;

        public void Clear()
        {
            _pipes.Clear();

            UpdatePipeline();
        }

        public AsyncPipeCollection<TContext> Add(Func<TContext, Func<TContext, Task>, Task> item)
        {
            _pipes.Add(new AsyncLambdaPipe<TContext>(item));

            UpdatePipeline();

            return this;
        }

        public AsyncPipeCollection<TContext> Add(IAsyncPipe<TContext> item)
        {
            _pipes.Add(item);

            UpdatePipeline();

            return this;
        }

        public AsyncPipeCollection<TContext> AddRange(IAsyncPipe<TContext>[] collection)
        {
            _pipes.AddRange(collection);

            UpdatePipeline();

            return this;
        }

        public AsyncPipeCollection<TContext> AddRange(params Func<TContext, Func<TContext, Task>, Task>[] collection)
        {
            collection.Each(x => _pipes.Add(new AsyncLambdaPipe<TContext>(x)));

            UpdatePipeline();

            return this;
        }

        public AsyncPipeCollection<TContext> Insert(int index, IAsyncPipe<TContext> item)
        {
            _pipes.Insert(index, item);

            UpdatePipeline();

            return this;
        }

        public int IndexOf(IAsyncPipe<TContext> item)
        {
            return _pipes.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            _pipes.RemoveAt(index);

            UpdatePipeline();
        }

        public bool Contains(IAsyncPipe<TContext> item)
        {
            return _pipes.Contains(item);
        }

        public void CopyTo(IAsyncPipe<TContext>[] array, int arrayIndex)
        {
            _pipes.CopyTo(array, arrayIndex);
        }

        public bool Remove(IAsyncPipe<TContext> item)
        {
            var removed = _pipes.Remove(item);

            UpdatePipeline();

            return removed;
        }

        public IEnumerator<IAsyncPipe<TContext>> GetEnumerator()
        {
            return _pipes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IAsyncPipe<TContext> this[int index] { get => _pipes[index]; set => _pipes[index] = value; }

        public static implicit operator AsyncPipeCollection<TContext>(Func<TContext, Func<TContext, Task>, Task>[] pipes)
        {
            var ctx = new AsyncPipeCollection<TContext>();

            ctx.AddRange(pipes);

            return ctx;
        }

        protected virtual void UpdatePipeline()
        {
            _pipeline = new Lazy<IAsyncPipe<TContext>>(() => PipelineWizard.Create(_pipes.ToArray()));
        }

        void IList<IAsyncPipe<TContext>>.Insert(int index, IAsyncPipe<TContext> item)
        {
            Insert(index, item);
        }

        void ICollection<IAsyncPipe<TContext>>.Add(IAsyncPipe<TContext> item)
        {
            Add(item);
        }
    }
}