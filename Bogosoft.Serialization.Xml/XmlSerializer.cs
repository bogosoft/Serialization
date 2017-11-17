using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ActualSerializer = System.Xml.Serialization.XmlSerializer;

namespace Bogosoft.Serialization.Xml
{
    /// <summary>
    /// An XML serialization/deserialization strategy.
    /// </summary>
    public class XmlSerializer :
        IAsyncDeserializer<Stream>,
        IAsyncDeserializer<TextReader>,
        IAsyncDeserializer<XmlReader>,
        IAsyncSerializer<Stream>,
        IAsyncSerializer<TextWriter>,
        IAsyncSerializer<XmlWriter>,
        IDeserializer<Stream>,
        IDeserializer<TextReader>,
        IDeserializer<XmlReader>,
        ISerializer<Stream>,
        ISerializer<TextWriter>,
        ISerializer<XmlWriter>
    {
        /// <summary>
        /// Deserialize a given source of XML data.
        /// </summary>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <returns>An object of the given type.</returns>
        public TOut Deserialize<TOut>(Stream source)
        {
            return (TOut)new ActualSerializer(typeof(TOut)).Deserialize(source);
        }

        /// <summary>
        /// Deserialize a given source of XML data.
        /// </summary>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <returns>An object of the given type.</returns>
        public TOut Deserialize<TOut>(TextReader source)
        {
            return (TOut)new ActualSerializer(typeof(TOut)).Deserialize(source);
        }

        /// <summary>
        /// Deserialize a given source of XML data.
        /// </summary>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <returns>An object of the given type.</returns>
        public TOut Deserialize<TOut>(XmlReader source)
        {
            return (TOut)new ActualSerializer(typeof(TOut)).Deserialize(source);
        }

        /// <summary>
        /// Deserialize a given source of XML data.
        /// </summary>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public Task<TOut> DeserializeAsync<TOut>(Stream source, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task.FromResult((TOut)new ActualSerializer(typeof(TOut)).Deserialize(source));
        }

        /// <summary>
        /// Deserialize a given source of XML data.
        /// </summary>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public Task<TOut> DeserializeAsync<TOut>(TextReader source, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task.FromResult((TOut)new ActualSerializer(typeof(TOut)).Deserialize(source));
        }

        /// <summary>
        /// Deserialize a given source of XML data.
        /// </summary>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <param name="source">The source of the data to deserialize.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the given type.</returns>
        public Task<TOut> DeserializeAsync<TOut>(XmlReader source, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task.FromResult((TOut)new ActualSerializer(typeof(TOut)).Deserialize(source));
        }

        /// <summary>
        /// XML serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        public void Serialize(object data, Stream destination)
        {
            new ActualSerializer(data.GetType()).Serialize(destination, data);
        }

        /// <summary>
        /// XML serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        public void Serialize(object data, TextWriter destination)
        {
            new ActualSerializer(data.GetType()).Serialize(destination, data);
        }

        /// <summary>
        /// XML serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        public void Serialize(object data, XmlWriter destination)
        {
            new ActualSerializer(data.GetType()).Serialize(destination, data);
        }

        /// <summary>
        /// XML serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeAsync(object data, Stream destination, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            new ActualSerializer(data.GetType()).Serialize(destination, data);

            return Task.FromResult(0);
        }

        /// <summary>
        /// XML serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeAsync(object data, TextWriter destination, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            new ActualSerializer(data.GetType()).Serialize(destination, data);

            return Task.FromResult(0);
        }

        /// <summary>
        /// XML serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeAsync(object data, XmlWriter destination, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            new ActualSerializer(data.GetType()).Serialize(destination, data);

            return Task.FromResult(0);
        }
    }
}