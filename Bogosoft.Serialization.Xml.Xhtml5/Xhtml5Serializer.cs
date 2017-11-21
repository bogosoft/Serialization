using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Serialization.Xml.Xhtml5
{
    /// <summary>
    /// A specialized implementation of the <see cref="ISerializer{T}"/> and <see cref="IAsyncSerializer{T}"/>
    /// contracts suited to correctly outputting the XML serialization of HTML 5 (XHTML 5).
    /// </summary>
    public class Xhtml5Serializer : XmlDocumentSerializer
    {
        /// <summary>
        /// Get an array of element names that indicate that their respective elements
        /// should not self-close.
        /// </summary>
        protected readonly static String[] ShouldNotSelfClose = new String[]
        {
            "b",
            "div",
            "i",
            "p",
            "script",
            "span",
            "td",
            "textarea"
        };

        /// <summary>
        /// Serialize a given XML document to a given text writer.
        /// </summary>
        /// <param name="document">An XML document to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document to.</param>
        protected override void SerializeDocument(XmlDocument document, TextWriter output)
        {
            if (document.DocumentElement?.Name == "html")
            {
                output.Write("<!DOCTYPE html>");
            }

            base.SerializeDocument(document, output);
        }

        /// <summary>
        /// Serialize a given XML document to a given text writer.
        /// </summary>
        /// <param name="document">An XML document to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected override async Task SerializeDocumentAsync(
            XmlDocument document,
            TextWriter output,
            CancellationToken token
            )
        {
            if (document.DocumentElement?.Name == "html")
            {
                await output.WriteAsync("<!DOCTYPE html>", token).ConfigureAwait(false);
            }

            await base.SerializeDocumentAsync(document, output, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Serialize a given XML document type to a given text writer. This overload does nothing.
        /// </summary>
        /// <param name="doctype">An XML document type to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document type to.</param>
        protected override void SerializeDocumentType(XmlDocumentType doctype, TextWriter output)
        {
        }

        /// <summary>
        /// Serialize a given XML document type to a given text writer. This overload does nothing.
        /// </summary>
        /// <param name="doctype">An XML document type to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document type to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected override Task SerializeDocumentTypeAsync(
            XmlDocumentType doctype,
            TextWriter output,
            CancellationToken token
            )
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Serialize a given XML element to a given text writer.
        /// </summary>
        /// <param name="element">An XML element to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML element to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        protected override void SerializeElement(XmlElement element, TextWriter output, string indent)
        {
            if (!element.HasChildNodes && ShouldNotSelfClose.Contains(element.Name))
            {
                output.Write(LBreak + indent + "<" + element.Name);

                foreach (XmlAttribute a in element.Attributes)
                {
                    SerializeAttribute(a, output, indent);
                }

                output.Write("></" + element.Name + ">");
            }
            else
            {
                base.SerializeElement(element, output, indent);
            }
        }

        /// <summary>
        /// Serialize a given XML element to a given text writer.
        /// </summary>
        /// <param name="element">An XML element to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML element to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected override async Task SerializeElementAsync(
            XmlElement element,
            TextWriter output,
            string indent,
            CancellationToken token
            )
        {
            if (!element.HasChildNodes && ShouldNotSelfClose.Contains(element.Name))
            {
                await output.WriteAsync(LBreak + indent + "<" + element.Name, token).ConfigureAwait(false);

                foreach (XmlAttribute a in element.Attributes)
                {
                    await SerializeAttributeAsync(a, output, indent, token).ConfigureAwait(false);
                }

                await output.WriteAsync("></" + element.Name + ">", token).ConfigureAwait(false);
            }
            else
            {
                await base.SerializeElementAsync(element, output, indent, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Serialize a given XML declaration to a given text writer. This overload does nothing.
        /// </summary>
        /// <param name="declaration">An XML declaration to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML declaration to.</param>
        protected override void SerializeXmlDeclaration(XmlDeclaration declaration, TextWriter output)
        {
        }

        /// <summary>
        /// Serialize a given XML declaration to a given text writer. This overload does nothing.
        /// </summary>
        /// <param name="declaration">An XML declaration to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML declaration to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected override Task SerializeXmlDeclarationAsync(
            XmlDeclaration declaration,
            TextWriter output,
            CancellationToken token
            )
        {
            return Task.FromResult(0);
        }
    }
}