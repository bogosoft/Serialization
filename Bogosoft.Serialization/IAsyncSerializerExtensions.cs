using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization
{
    /// <summary>
    /// Extended functionality for the <see cref="IAsyncSerializer{T}"/> contract.
    /// </summary>
    public static class IAsyncSerializerExtensions
    {
        /// <summary>
        /// Serialize a given object to a given destination.
        /// </summary>
        /// <typeparam name="T">The type of the destination to serialize to.</typeparam>
        /// <param name="serializer">The current serializer.</param>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public static Task SerializeAsync<T>(this IAsyncSerializer<T> serializer, object data, T destination)
        {
            return serializer.SerializeAsync(data, destination, CancellationToken.None);
        }

        /// <summary>
        /// Serialize a given object of the specified type to a given destination.
        /// </summary>
        /// <typeparam name="TIn">The type of the object that can be serialized.</typeparam>
        /// <typeparam name="TDestination">The type of the destination to serialize to.</typeparam>
        /// <param name="serializer">The current serializer.</param>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
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