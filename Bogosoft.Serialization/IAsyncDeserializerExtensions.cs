using System.IO;
using System.Text;
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
        /// Deserialize a given source of data. During the deserialization operation, byte order will be
        /// automatically detected, the default buffer size will be used, and the stream encoding will
        /// be assumed to be <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source to deserialize from.</typeparam>
        /// <param name="deserializer">The current deserializer.</param>
        /// <param name="source">A stream of data to deserialize from.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public static async Task<T> DeserializeAsync<T>(
            this IAsyncDeserializer<TextReader> deserializer,
            Stream source,
            CancellationToken token = default(CancellationToken)
            )
        {
            using (var reader = new StreamReader(source, Encoding.UTF8, true))
            {
                return await deserializer.DeserializeAsync<T>(reader, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Deserialize a given source of data. During the deserialization operation, byte order will be
        /// automatically detected and the default buffer size will be used.
        /// </summary>
        /// <typeparam name="T">The type of the source to deserialize from.</typeparam>
        /// <param name="deserializer">The current deserializer.</param>
        /// <param name="source">A stream of data to deserialize from.</param>
        /// <param name="encoding">The encoding to use during deserialization.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public static async Task<T> DeserializeAsync<T>(
            this IAsyncDeserializer<TextReader> deserializer,
            Stream source,
            Encoding encoding,
            CancellationToken token = default(CancellationToken)
            )
        {
            using (var reader = new StreamReader(source, encoding, true))
            {
                return await deserializer.DeserializeAsync<T>(reader, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Deserialize a given source of data. During the deserialization operation,
        /// byte order will be automatically detected.
        /// </summary>
        /// <typeparam name="T">The type of the source to deserialize from.</typeparam>
        /// <param name="deserializer">The current deserializer.</param>
        /// <param name="source">A stream of data to deserialize from.</param>
        /// <param name="encoding">The encoding to use during deserialization.</param>
        /// <param name="bufferSize">
        /// A value corresponding to the size of the buffer to use during serialization, in bytes.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public static async Task<T> DeserializeAsync<T>(
            this IAsyncDeserializer<TextReader> deserializer,
            Stream source,
            Encoding encoding,
            int bufferSize,
            CancellationToken token = default(CancellationToken)
            )
        {
            using (var reader = new StreamReader(source, encoding, true, bufferSize))
            {
                return await deserializer.DeserializeAsync<T>(reader, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Deserialize a given source of data.
        /// </summary>
        /// <typeparam name="T">The type of the source to deserialize from.</typeparam>
        /// <param name="deserializer">The current deserializer.</param>
        /// <param name="source">A stream of data to deserialize from.</param>
        /// <param name="encoding">The encoding to use during deserialization.</param>
        /// <param name="autodetect">
        /// A value indicating whether or not the byte order of the stream should
        /// be automatically detected during deserialization.
        /// </param>
        /// <param name="bufferSize">
        /// A value corresponding to the size of the buffer to use during serialization, in bytes.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public static async Task<T> DeserializeAsync<T>(
            this IAsyncDeserializer<TextReader> deserializer,
            Stream source,
            Encoding encoding,
            bool autodetect,
            int bufferSize,
            CancellationToken token = default(CancellationToken)
            )
        {
            using (var reader = new StreamReader(source, encoding, autodetect, bufferSize))
            {
                return await deserializer.DeserializeAsync<T>(reader, token).ConfigureAwait(false);
            }
        }

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