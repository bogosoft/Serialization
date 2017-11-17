using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization
{
    /// <summary>
    /// A template for any type capable of deserializing data from a source of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the source.</typeparam>
    public interface IAsyncDeserializer<T>
    {
        /// <summary>
        /// Deserialize a given source into an object.
        /// </summary>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object deserialized from the given source.</returns>
        Task<object> DeserializeAsync(T source, CancellationToken token);
    }

    /// <summary>
    /// A template for any type capable of deserializing data from a source of a specified type.
    /// </summary>
    /// <typeparam name="TOut">The type of the object to deserialize from the source.</typeparam>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    public interface IAsyncDeserializer<TOut, TSource>
    {
        /// <summary>
        /// Deserialize a given source into an object of the specified output type.
        /// </summary>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token"></param>
        /// <returns>An object of the output type deserialized from the given source.</returns>
        Task<TOut> DeserializeAsync(TSource source, CancellationToken token);
    }
}