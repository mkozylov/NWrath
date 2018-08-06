namespace NWrath.Synergy.Common.Structs
{
    public class Ref<TValue>
        where TValue : class
    {
        public static Ref<TValue> Empty { get; } = new Ref<TValue>(default(TValue));

        public TValue Value { get; set; }

        public bool HasValue { get { return Value != null; } }

        public Ref(TValue value)
        {
            Value = value;
        }
    }
}