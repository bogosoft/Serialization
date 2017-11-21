using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization.Xml
{
    static class TextWriterExtensions
    {
        internal static Task WriteAsync(this TextWriter writer, string value, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return writer.WriteAsync(value);
        }
    }
}