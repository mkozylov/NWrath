using NWrath.Synergy.Common.Extensions.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NWrath.Synergy.Pipeline
{
    public class PipeCollection<TContext>
        : IList<IPipe<TContext>>
        where TContext : class
    {
        public IPipe<TContext> Pipeline { get => _pipeline.Value; }

        public int Count => _pipes.Count;

        public bool IsReadOnly { get; } = false;

        private List<IPipe<TContext>> _pipes = new List<IPipe<TContext>>();
        private Lazy<IPipe<TContext>> _pipeline;

        public PipeCollection(params Action<TContext, Action<TContext>>[] pipes)
        {
            AddRange(pipes);
        }

        public PipeCollection(IEnumerable<IPipe<TContext>> pipes)
        {
            AddRange(pipes.ToArray());
        }

        public void Clear()
        {
            _pipes.Clear();

            UpdatePipeline();
        }

        public PipeCollection<TContext> Add(Action<TContext, Action<TContext>> item)
        {
            _pipes.Add(new LambdaPipe<TContext>(item));

            UpdatePipeline();

            return this;
        }

        public PipeCollection<TContext> Add(IPipe<TContext> item)
        {
            _pipes.Add(item);

            UpdatePipeline();

            return this;
        }

        public PipeCollection<TContext> AddRange(IPipe<TContext>[] collection)
        {
            _pipes.AddRange(collection);

            UpdatePipeline();

            return this;
        }

        public PipeCollection<TContext> AddRange(params Action<TContext, Action<TContext>>[] collection)
        {
            collection.Each(x => _pipes.Add(new LambdaPipe<TContext>(x)));

            UpdatePipeline();

            return this;
        }

        public PipeCollection<TContext> Insert(int index, IPipe<TContext> item)
        {
            _pipes.Insert(index, item);

            UpdatePipeline();

            return this;
        }

        public int IndexOf(IPipe<TContext> item)
        {
            return _pipes.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            _pipes.RemoveAt(index);

            UpdatePipeline();
        }

        public bool Contains(IPipe<TContext> item)
        {
            return _pipes.Contains(item);
        }

        public void CopyTo(IPipe<TContext>[] array, int arrayIndex)
        {
            _pipes.CopyTo(array, arrayIndex);
        }

        public bool Remove(IPipe<TContext> item)
        {
            var removed = _pipes.Remove(item);

            UpdatePipeline();

            return removed;
        }

        public IEnumerator<IPipe<TContext>> GetEnumerator()
        {
            return _pipes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IPipe<TContext> this[int index] { get => _pipes[index]; set => _pipes[index] = value; }

        public static implicit operator PipeCollection<TContext>(Action<TContext, Action<TContext>>[] pipes)
        {
            var ctx = new PipeCollection<TContext>();

            ctx.AddRange(pipes);

            return ctx;
        }

        protected virtual void UpdatePipeline()
        {
            _pipeline = new Lazy<IPipe<TContext>>(() => PipelineWizard.Create(_pipes.ToArray()));
        }

        void IList<IPipe<TContext>>.Insert(int index, IPipe<TContext> item)
        {
            Insert(index, item);
        }

        void ICollection<IPipe<TContext>>.Add(IPipe<TContext> item)
        {
            Add(item);
        }
    }
}