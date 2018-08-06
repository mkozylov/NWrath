using System.Threading.Tasks;

namespace NWrath.Synergy.Pipeline
{
    public interface IAsyncPipe<TContext>
    {
        IAsyncPipe<TContext> Next { get; set; }

        Task Perform(TContext context);
    }
}