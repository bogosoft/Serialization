using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization
{
    public static class IAsyncDeserializerExtensions
    {
        public static Task<object> DeserializeAsync<T>(this IAsyncDeserializer<T> deserializer, T source)
        {
            return deserializer.DeserializeAsync(source, CancellationToken.None);
        }

        public static async Task<TOut> DeserializeAsync<TSource, TOut>(
            this IAsyncDeserializer<TSource> deserializer,
            TSource source,
            CancellationToken token = default(CancellationToken)
            )
        {
            return (TOut)await deserializer.DeserializeAsync(source, token).ConfigureAwait(false);
        }

        public static Task<TOut> DeserializeAsync<TOut, TSource>(
            this IAsyncDeserializer<TOut, TSource> deserializer,
            TSource source
            )
        {
            return deserializer.DeserializeAsync(source, CancellationToken.None);
        }
    }
}