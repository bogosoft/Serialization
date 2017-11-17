using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization
{
    /// <summary>
    /// A template for any type capable of deserializing data from a source of a specified type.
    /// </summary>
    /// <typeparam name="TSource">The type of the source to deserialize from.</typeparam>
    public interface IAsyncDeserializer<TSource>
    {
        /// <summary>
        /// Deserialize a given source of data.
        /// </summary>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        Task<TOut> DeserializeAsync<TOut>(TSource source, CancellationToken token);
    }
}