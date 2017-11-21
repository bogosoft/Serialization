using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Serialization.Xml
{
    /// <summary>
    /// A type capable of serializing <see cref="XmlDocument"/> objects to <see cref="TextWriter"/> destinations.
    /// </summary>
    public class XmlNodeSerializer :
        IAsyncSerializer<XmlNode, TextWriter>,
        ISerializer<XmlNode, TextWriter>
    {
        /// <summary>
        /// Get or set the base indent sequence to be used during serialization.
        /// </summary>
        public string Indent = "\t";

        /// <summary>
        /// Get or set the line break sequence to be used during serialization.
        /// </summary>
        public string LBreak = "\r\n";

        /// <summary>
        /// Serialize a given XML document to a given text writer.
        /// </summary>
        /// <param name="data">An XML document to serialize.</param>
        /// <param name="destination">A destination text writer to serialize to.</param>
        public void Serialize(XmlNode data, TextWriter destination)
        {
            SerializeNode(data, destination, "");
        }

        /// <summary>
        /// Serialize a given XML attribute.
        /// </summary>
        /// <param name="attribute">An XML attribute to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML attribute to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        protected virtual void SerializeAttribute(XmlAttribute attribute, TextWriter output, string indent)
        {
            output.Write($@" {attribute.Name}=""{attribute.Value}""");
        }

        /// <summary>
        /// Serialize a given XML CDATA section.
        /// </summary>
        /// <param name="cdata">An XML CDATA section to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML CDATA section to.</param>
        protected virtual void SerializeCDataSection(XmlCDataSection cdata, TextWriter output)
        {
            output.Write(cdata.OuterXml);
        }

        /// <summary>
        /// Serialize a given XML comment.
        /// </summary>
        /// <param name="comment">An XML comment to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML comment to.</param>
        protected virtual void SerializeComment(XmlComment comment, TextWriter output)
        {
            output.Write(comment.OuterXml);
        }

        /// <summary>
        /// Serialize a given XML document.
        /// </summary>
        /// <param name="document">An XML document to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document to.</param>
        protected virtual void SerializeDocument(XmlDocument document, TextWriter output)
        {
            foreach (XmlNode child in document.ChildNodes)
            {
                SerializeNode(child, output, "");
            }
        }

        /// <summary>
        /// Serialize a given XML document fragment.
        /// </summary>
        /// <param name="fragment">An XML document fragment to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document fragment to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        protected virtual void SerializeDocumentFragment(XmlDocumentFragment fragment, TextWriter output, string indent)
        {
            foreach (XmlNode child in fragment.ChildNodes)
            {
                SerializeNode(child, output, indent);
            }
        }

        /// <summary>
        /// Serialize a given XML document type.
        /// </summary>
        /// <param name="doctype">An XML document type to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document type to.</param>
        protected virtual void SerializeDocumentType(XmlDocumentType doctype, TextWriter output)
        {
            output.Write(doctype.OuterXml);
        }

        /// <summary>
        /// Serialize a given XML element.
        /// </summary>
        /// <param name="element">An XML element to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML element to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        protected virtual void SerializeElement(XmlElement element, TextWriter output, string indent)
        {
            output.Write(LBreak + indent + "<" + element.Name);

            foreach (XmlAttribute a in element.Attributes)
            {
                SerializeAttribute(a, output, indent);
            }

            if (!element.HasChildNodes)
            {
                output.Write(" />");

                return;
            }

            var ecount = 0;

            foreach (XmlNode child in element.ChildNodes)
            {
                SerializeNode(child, output, indent + Indent);

                if (child.NodeType == XmlNodeType.Element)
                {
                    ++ecount;
                }
            }

            if (ecount > 0)
            {
                output.Write(LBreak + indent);
            }

            output.Write($"</{element.Name}>");
        }

        /// <summary>
        /// Serialize a given XML node.
        /// </summary>
        /// <param name="node">An XML node to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML node to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        protected virtual void SerializeNode(XmlNode node, TextWriter output, string indent)
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Attribute:
                    SerializeAttribute(node as XmlAttribute, output, indent);
                    break;
                case XmlNodeType.CDATA:
                    SerializeCDataSection(node as XmlCDataSection, output);
                    break;
                case XmlNodeType.Comment:
                    SerializeComment(node as XmlComment, output);
                    break;
                case XmlNodeType.Document:
                    SerializeDocument(node as XmlDocument, output);
                    break;
                case XmlNodeType.DocumentFragment:
                    SerializeDocumentFragment(node as XmlDocumentFragment, output, indent);
                    break;
                case XmlNodeType.DocumentType:
                    SerializeDocumentType(node as XmlDocumentType, output);
                    break;
                case XmlNodeType.Element:
                    SerializeElement(node as XmlElement, output, indent);
                    break;
                case XmlNodeType.ProcessingInstruction:
                    SerializeProcessingInstruction(node as XmlProcessingInstruction, output);
                    break;
                case XmlNodeType.Text:
                    SerializeText(node as XmlText, output);
                    break;
                case XmlNodeType.XmlDeclaration:
                    SerializeXmlDeclaration(node as XmlDeclaration, output);
                    break;
                default:
                    output.Write(node.OuterXml);
                    break;
            }
        }

        /// <summary>
        /// Serialize a given XML processing instruction.
        /// </summary>
        /// <param name="pi">An XML processing instruction to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML processing instruction to.</param>
        protected virtual void SerializeProcessingInstruction(XmlProcessingInstruction pi, TextWriter output)
        {
            output.Write(pi.OuterXml);
        }

        /// <summary>
        /// Serialize a given XML declaration.
        /// </summary>
        /// <param name="declaration">An XML declaration to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML declaration to.</param>
        protected virtual void SerializeXmlDeclaration(XmlDeclaration declaration, TextWriter output)
        {
            output.Write(declaration.OuterXml);
        }

        /// <summary>
        /// Serialize a given text XML node.
        /// </summary>
        /// <param name="text">An XML text node to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML text node to.</param>
        protected virtual void SerializeText(XmlText text, TextWriter output)
        {
            output.Write(text.Data);
        }

        /// <summary>
        /// Serialize a given XML node to a given text writer.
        /// </summary>
        /// <param name="data">An XML node to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeAsync(XmlNode data, TextWriter destination, CancellationToken token)
        {
            return SerializeNodeAsync(data, destination, "", token);
        }

        /// <summary>
        /// Serialize a given XML attribute to a given text writer.
        /// </summary>
        /// <param name="attribute">An XML attribute to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML attribute to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual Task SerializeAttributeAsync(
            XmlAttribute attribute,
            TextWriter output,
            string indent,
            CancellationToken token
            )
        {
            return output.WriteAsync($@" {attribute.Name}=""{attribute.Value}""", token);
        }

        /// <summary>
        /// Serialize a given XML CDATA section to a given text writer.
        /// </summary>
        /// <param name="cdata">An XML CDATA section to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML CDATA section to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual Task SerializeCDataSectionAsync(
            XmlCDataSection cdata,
            TextWriter output,
            CancellationToken token
            )
        {
            return output.WriteAsync(cdata.OuterXml, token);
        }

        /// <summary>
        /// Serialize a given XML comment to a given text writer.
        /// </summary>
        /// <param name="comment">An XML comment to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML comment to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual Task SerializeCommentAsync(
            XmlComment comment,
            TextWriter output,
            CancellationToken token
            )
        {
            return output.WriteAsync(comment.OuterXml, token);
        }

        /// <summary>
        /// Serialize a given XML document to a given text writer.
        /// </summary>
        /// <param name="document">An XML document to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual async Task SerializeDocumentAsync(
            XmlDocument document,
            TextWriter output,
            CancellationToken token
            )
        {
            foreach (XmlNode child in document.ChildNodes)
            {
                await SerializeNodeAsync(child, output, "", token);
            }
        }

        /// <summary>
        /// Serialize a given XML document fragment to a given text writer.
        /// </summary>
        /// <param name="fragment">An XML document fragment to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document fragment to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual async Task SerializeDocumentFragmentAsync(
            XmlDocumentFragment fragment,
            TextWriter output,
            string indent,
            CancellationToken token
            )
        {
            foreach (XmlNode child in fragment.ChildNodes)
            {
                await SerializeNodeAsync(child, output, indent, token);
            }
        }

        /// <summary>
        /// Serialize a given XML document type to a given text writer.
        /// </summary>
        /// <param name="doctype">An XML document type to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML document type to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual Task SerializeDocumentTypeAsync(
            XmlDocumentType doctype,
            TextWriter output,
            CancellationToken token
            )
        {
            return output.WriteAsync(doctype.OuterXml, token);
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
        protected virtual async Task SerializeElementAsync(
            XmlElement element,
            TextWriter output,
            string indent,
            CancellationToken token
            )
        {
            await output.WriteAsync(LBreak + indent + "<" + element.Name, token);

            foreach (XmlAttribute a in element.Attributes)
            {
                await SerializeAttributeAsync(a, output, indent, token);
            }

            if (!element.HasChildNodes)
            {
                await output.WriteAsync(" />", token);

                return;
            }

            var ecount = 0;

            foreach (XmlNode child in element.ChildNodes)
            {
                await SerializeNodeAsync(child, output, indent + Indent, token);

                if (child.NodeType == XmlNodeType.Element)
                {
                    ++ecount;
                }
            }

            if (ecount > 0)
            {
                await output.WriteAsync(LBreak + indent, token);
            }

            await output.WriteAsync($"</{element.Name}>", token);
        }

        /// <summary>
        /// Serialize a given XML node to a given text writer.
        /// </summary>
        /// <param name="node">An XML node to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML node to.</param>
        /// <param name="indent">
        /// A value corresponding to the current level of indentation.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public Task SerializeNodeAsync(XmlNode node, TextWriter output, string indent, CancellationToken token)
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Attribute:
                    return SerializeAttributeAsync(node as XmlAttribute, output, indent, token);
                case XmlNodeType.CDATA:
                    return SerializeCDataSectionAsync(node as XmlCDataSection, output, token);
                case XmlNodeType.Comment:
                    return SerializeCommentAsync(node as XmlComment, output, token);
                case XmlNodeType.Document:
                    return SerializeDocumentAsync(node as XmlDocument, output, token);
                case XmlNodeType.DocumentFragment:
                    return SerializeDocumentFragmentAsync(node as XmlDocumentFragment, output, indent, token);
                case XmlNodeType.DocumentType:
                    return SerializeDocumentTypeAsync(node as XmlDocumentType, output, token);
                case XmlNodeType.Element:
                    return SerializeElementAsync(node as XmlElement, output, indent, token);
                case XmlNodeType.ProcessingInstruction:
                    return SerializeProcessingInstructionAsync(node as XmlProcessingInstruction, output, token);
                case XmlNodeType.Text:
                    return SerializeTextAsync(node as XmlText, output, token);
                case XmlNodeType.XmlDeclaration:
                    return SerializeXmlDeclarationAsync(node as XmlDeclaration, output, token);
                default:
                    return output.WriteAsync(node.OuterXml, token);
            }
        }

        /// <summary>
        /// Serialize a given XML processing instruction to a given text writer.
        /// </summary>
        /// <param name="pi">An XML processing instruction to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML processing instruction to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual Task SerializeProcessingInstructionAsync(
            XmlProcessingInstruction pi,
            TextWriter output,
            CancellationToken token
            )
        {
            return output.WriteAsync(pi.OuterXml, token);
        }

        /// <summary>
        /// Serialize a given XML text node to a given text writer.
        /// </summary>
        /// <param name="text">An XML text node to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML text node to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual Task SerializeTextAsync(XmlText text, TextWriter output, CancellationToken token)
        {
            return output.WriteAsync(text.Data, token);
        }

        /// <summary>
        /// Serialize a given XML declaration to a given text writer.
        /// </summary>
        /// <param name="declaration">An XML declaration to serialize.</param>
        /// <param name="output">A text writer to serialize a given XML declaration to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        protected virtual Task SerializeXmlDeclarationAsync(
            XmlDeclaration declaration,
            TextWriter output,
            CancellationToken token
            )
        {
            return output.WriteAsync(declaration.OuterXml, token);
        }
    }
}