using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization
{
    public static class IAsyncSerializerExtensions
    {
        public static Task SerializeAsync<T>(this IAsyncSerializer<T> serializer, object data, T destination)
        {
            return serializer.SerializeAsync(data, destination, CancellationToken.None);
        }

        public static Task SerializeAsync<TIn, TDestination>(
            this IAsyncSerializer<TIn, TDestination> serializer,
            TIn data,
            TDestination destination
            )
        {
            return serializer.SerializeAsync(data, destination, CancellationToken.None);
        }
    }
}