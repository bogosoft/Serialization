using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization.Json
{
    public sealed class JsonSerializer :
        IAsyncDeserializer<TextReader>,
        IAsyncSerializer<TextWriter>,
        IDeserializer<TextReader>,
        ISerializer<TextWriter>
    {
        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

        public object Deserialize(TextReader source)
        {
            using (var reader = new JsonTextReader(source))
            {
                return serializer.Deserialize(reader);
            }
        }

        public Task<object> DeserializeAsync(TextReader source, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using (var reader = new JsonTextReader(source))
            {
                return Task.FromResult(serializer.Deserialize(reader));
            }
        }

        public void Serialize(object data, TextWriter destination)
        {
            using (var writer = new JsonTextWriter(destination))
            {
                serializer.Serialize(writer, data);
            }
        }

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