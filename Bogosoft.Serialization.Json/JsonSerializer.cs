using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization.Json
{
    /// <summary>
    /// A JSON serializer/deserializer.
    /// </summary>
    public sealed class JsonSerializer :
        IAsyncDeserializer<TextReader>,
        IAsyncSerializer<TextWriter>,
        IDeserializer<TextReader>,
        ISerializer<TextWriter>
    {
        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

        /// <summary>
        /// Deserialize a given source of JSON data.
        /// </summary>
        /// <typeparam name="T">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <returns>An object of the given type.</returns>
        public T Deserialize<T>(TextReader source)
        {
            using (var reader = new JsonTextReader(source))
            {
                return serializer.Deserialize<T>(reader);
            }
        }

        /// <summary>
        /// Deserialize a given source of JSON data.
        /// </summary>
        /// <typeparam name="T">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public Task<T> DeserializeAsync<T>(TextReader source, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using (var reader = new JsonTextReader(source))
            {
                return Task.FromResult(serializer.Deserialize<T>(reader));
            }
        }

        /// <summary>
        /// JSON serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        public void Serialize(object data, TextWriter destination)
        {
            using (var writer = new JsonTextWriter(destination))
            {
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// JSON serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeAsync(object data, TextWriter destination, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using (var writer = new JsonTextWriter(destination))
            {
                serializer.Serialize(writer, data);
            }

            return Task.FromResult(0);
        }
    }
}