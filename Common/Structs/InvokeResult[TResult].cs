using System;

namespace NWrath.Synergy.Common.Structs
{
    public class InvokeResult<TResult>
        : InvokeResult
    {
        public TResult Value { get; set; }

        public InvokeResult()
            : base()
        {
        }

        public InvokeResult(TResult value)
        {
            Value = value;
        }
    }

    public class InvokeResult
    {
        public Exception Error { get; set; }

        public bool HasError { get => Error != null; }

        public InvokeResult()
        {
        }
    }
}
