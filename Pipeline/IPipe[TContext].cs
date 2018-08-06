namespace NWrath.Synergy.Pipeline
{
    public interface IPipe<TContext>
    {
        IPipe<TContext> Next { get; set; }

        void Perform(TContext context);
    }
}