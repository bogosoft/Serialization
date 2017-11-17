using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization
{
    /// <summary>
    /// Extended functionality for the <see cref="IAsyncDeserializer{T}"/> contract.
    /// </summary>
    public static class IAsyncDeserializerExtensions
    {
        /// <summary>
        /// Deserialize a given source of data.
        /// </summary>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <typeparam name="TSource">The type of the source to deserialize from.</typeparam>
        /// <param name="deserializer">The current deserializer.</param>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <returns>An object of the given type.</returns>
        public static Task<TOut> DeserializeAsync<TOut, TSource>(
            this IAsyncDeserializer<TSource> deserializer,
            TSource source
            )
        {
            return deserializer.DeserializeAsync<TOut>(source, CancellationToken.None);
        }
    }
}