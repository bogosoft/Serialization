using System.IO;
using System.Text;

namespace Bogosoft.Serialization
{
    /// <summary>
    /// Extended functionality for the <see cref="ISerializer{T}"/> contract.
    /// </summary>
    public static class ISerializerExtensions
    {
        /// <summary>
        /// Serialize a given object to a given stream. The encoding to be used
        /// during serialization is <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="serializer">The current serializer.</param>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A stream to serialize to.</param>
        public static void Serialize(
            this ISerializer<TextWriter> serializer,
            object data,
            Stream destination
            )
        {
            using (var writer = new StreamWriter(destination))
            {
                serializer.Serialize(data, writer);
            }
        }

        /// <summary>
        /// Serialize a given object to a given stream.
        /// </summary>
        /// <param name="serializer">The current serializer.</param>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A stream to serialize to.</param>
        /// <param name="encoding">An encoding to use during serialization.</param>
        public static void Serialize(
            this ISerializer<TextWriter> serializer,
            object data,
            Stream destination,
            Encoding encoding
            )
        {
            using (var writer = new StreamWriter(destination, encoding))
            {
                serializer.Serialize(data, writer);
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
        public static void Serialize(
            this ISerializer<TextWriter> serializer,
            object data,
            Stream destination,
            Encoding encoding,
            int bufferSize
            )
        {
            using (var writer = new StreamWriter(destination, encoding, bufferSize))
            {
                serializer.Serialize(data, writer);
            }
        }
    }
}