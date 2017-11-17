using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization
{
    public interface IAsyncSerializer<T>
    {
        Task SerializeAsync(object data, T destination, CancellationToken token);
    }

    public interface IAsyncSerializer<in TIn, TDestination>
    {
        Task SerializeAsync(TIn data, TDestination destination, CancellationToken token);
    }
}