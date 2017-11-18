using System.IO;
using System.Text;
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
        /// Serialize a given object to a given stream The encoding to be used
        /// during serialization will be <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="serializer">The current serializer.</param>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A stream to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public static async Task SerializeAsync(
            this IAsyncSerializer<TextWriter> serializer,
            object data,
            Stream destination,
            CancellationToken token = default(CancellationToken)
            )
        {
            using (var writer = new StreamWriter(destination, Encoding.UTF8))
            {
                await serializer.SerializeAsync(data, writer, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Serialize a given object to a given stream.
        /// </summary>
        /// <param name="serializer">The current serializer.</param>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A stream to serialize to.</param>
        /// <param name="encoding">An encoding to use during serialization.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public static async Task SerializeAsync(
            this IAsyncSerializer<TextWriter> serializer,
            object data,
            Stream destination,
            Encoding encoding,
            CancellationToken token = default(CancellationToken)
            )
        {
            using (var writer = new StreamWriter(destination, encoding))
            {
                await serializer.SerializeAsync(data, writer, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Serialize a given object to a given stream.
        /// </summary>
        /// <param name="serializer">The current serializer.</param>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A stream to serialize to.</param>
        /// <param name="encoding">An encoding to use during serialization.</param>
        /// <param name="bufferSize">
        /// A value corresponding to the size of the buffer to use during serialization, in bytes.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public static async Task SerializeAsync(
            this IAsyncSerializer<TextWriter> serializer,
            object data,
            Stream destination,
            Encoding encoding,
            int bufferSize,
            CancellationToken token = default(CancellationToken)
            )
        {
            using (var writer = new StreamWriter(destination, encoding, bufferSize))
            {
                await serializer.SerializeAsync(data, writer, token).ConfigureAwait(false);
            }
        }

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