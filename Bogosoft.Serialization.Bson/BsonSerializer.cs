using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization.Bson
{
    /// <summary>
    /// A BSON data serializer/deserializer.
    /// </summary>
    public sealed class BsonSerializer :
        IAsyncDeserializer<BinaryReader>,
        IAsyncDeserializer<Stream>,
        IAsyncSerializer<BinaryWriter>,
        IAsyncSerializer<Stream>,
        IDeserializer<BinaryReader>,
        IDeserializer<Stream>,
        ISerializer<BinaryWriter>,
        ISerializer<Stream>
    {
        JsonSerializer serializer = new JsonSerializer();

        /// <summary>
        /// Deserialize a given source of BSON data.
        /// </summary>
        /// <typeparam name="T">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <returns>An object of the given type.</returns>
        public T Deserialize<T>(BinaryReader source)
        {
            using (var reader = new BsonDataReader(source))
            {
                reader.ReadRootValueAsArray = typeof(IEnumerable<>).IsAssignableFrom(typeof(T));

                return serializer.Deserialize<T>(reader);
            }
        }

        /// <summary>
        /// Deserialize a given source of BSON data.
        /// </summary>
        /// <typeparam name="T">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <returns>An object of the given type.</returns>
        public T Deserialize<T>(Stream source)
        {
            using (var reader = new BsonDataReader(source))
            {
                reader.ReadRootValueAsArray = typeof(IEnumerable<>).IsAssignableFrom(typeof(T));

                return serializer.Deserialize<T>(reader);
            }
        }

        /// <summary>
        /// Deserialize a given source of BSON data.
        /// </summary>
        /// <typeparam name="T">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public Task<T> DeserializeAsync<T>(BinaryReader source, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using (var reader = new BsonDataReader(source))
            {
                reader.ReadRootValueAsArray = typeof(IEnumerable<>).IsAssignableFrom(typeof(T));

                return Task.FromResult(serializer.Deserialize<T>(reader));
            }
        }

        /// <summary>
        /// Deserialize a given source of BSON data.
        /// </summary>
        /// <typeparam name="T">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public Task<T> DeserializeAsync<T>(Stream source, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using (var reader = new BsonDataReader(source))
            {
                reader.ReadRootValueAsArray = typeof(IEnumerable<>).IsAssignableFrom(typeof(T));

                return Task.FromResult(serializer.Deserialize<T>(reader));
            }
        }

        /// <summary>
        /// BSON serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        public void Serialize(object data, Stream destination)
        {
            using (var writer = new BsonDataWriter(destination))
            {
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// BSON serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        public void Serialize(object data, BinaryWriter destination)
        {
            using (var writer = new BsonDataWriter(destination))
            {
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// BSON serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeAsync(object data, BinaryWriter destination, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using (var writer = new BsonDataWriter(destination))
            {
                serializer.Serialize(writer, data);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// BSON serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeAsync(object data, Stream destination, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using (var writer = new BsonDataWriter(destination))
            {
                serializer.Serialize(writer, data);
            }

            return Task.FromResult(0);
        }
    }
}