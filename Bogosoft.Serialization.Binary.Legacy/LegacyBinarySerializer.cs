using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization.Binary.Legacy
{
    /// <summary>
    /// A serialization strategy that wraps .NET legacy binary formatting functionality.
    /// </summary>
    public sealed class LegacyBinarySerializer :
        IAsyncDeserializer<Stream>,
        IAsyncSerializer<Stream>,
        IDeserializer<Stream>,
        ISerializer<Stream>
    {
        Func<IFormatter> provider;

        /// <summary>
        /// Create a new instance of the <see cref="LegacyBinarySerializer"/> class. The underlying
        /// formatter that will be used during serialization and deserialization operations will
        /// be an instance of the <see cref="BinaryFormatter"/> class with deafult parameters.
        /// </summary>
        public LegacyBinarySerializer()
        {
            provider = () => new BinaryFormatter();
        }

        /// <summary>
        /// Create a new instance of the <see cref="LegacyBinarySerializer"/> class.
        /// </summary>
        /// <param name="provider">
        /// A delegate which will provide the underlying formatter during serialization
        /// and deserialization operations.
        /// </param>
        public LegacyBinarySerializer(Func<IFormatter> provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Deserialize a given stream of data.
        /// </summary>
        /// <typeparam name="T">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <returns>An object of the given type.</returns>
        public T Deserialize<T>(Stream source)
        {
            return (T)provider.Invoke().Deserialize(source);
        }

        /// <summary>
        /// Deserialize a given stream of data.
        /// </summary>
        /// <typeparam name="T">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public Task<T> DeserializeAsync<T>(Stream source, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task.FromResult((T)provider.Invoke().Deserialize(source));
        }

        /// <summary>
        /// Serialize a given object to a given stream.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination stream to serialize to.</param>
        public void Serialize(object data, Stream destination)
        {
            provider.Invoke().Serialize(destination, data);
        }

        /// <summary>
        /// Serialize a given object to a given stream.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination stream to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeAsync(object data, Stream destination, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            provider.Invoke().Serialize(destination, data);

            return Task.FromResult(0);
        }
    }
}