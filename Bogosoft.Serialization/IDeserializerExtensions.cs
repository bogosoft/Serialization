using System.IO;
using System.Text;

namespace Bogosoft.Serialization
{
    /// <summary>
    /// Extended functionality for the <see cref="IDeserializer{TSource}"/> contract.
    /// </summary>
    public static class IDeserializerExtensions
    {
        /// <summary>
        /// Deserialize a given source of data. During the deserialization operation, byte order will be
        /// automatically detected, the default buffer size will be used, and the stream encoding will
        /// be assumed to be <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source to deserialize from.</typeparam>
        /// <param name="deserializer">The current deserializer.</param>
        /// <param name="source">A stream of data to deserialize from.</param>
        /// <returns>An object of the given type.</returns>
        public static T Deserialize<T>(
            this IDeserializer<TextReader> deserializer,
            Stream source)
        {
            using (var reader = new StreamReader(source, Encoding.UTF8, true))
            {
                return deserializer.Deserialize<T>(reader);
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
        /// <returns>An object of the given type.</returns>
        public static T Deserialize<T>(
            this IDeserializer<TextReader> deserializer,
            Stream source,
            Encoding encoding
            )
        {
            using (var reader = new StreamReader(source, encoding, true))
            {
                return deserializer.Deserialize<T>(reader);
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
        /// <returns>An object of the given type.</returns>
        public static T Deserialize<T>(
            this IDeserializer<TextReader> deserializer,
            Stream source,
            Encoding encoding,
            int bufferSize
            )
        {
            using (var reader = new StreamReader(source, encoding, true, bufferSize))
            {
                return deserializer.Deserialize<T>(reader);
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
        /// <returns>An object of the given type.</returns>
        public static T Deserialize<T>(
            this IDeserializer<TextReader> deserializer,
            Stream source,
            Encoding encoding,
            bool autodetect,
            int bufferSize
            )
        {
            using (var reader = new StreamReader(source, encoding, autodetect, bufferSize))
            {
                return deserializer.Deserialize<T>(reader);
            }
        }
    }
}