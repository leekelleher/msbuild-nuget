using System.Xml;
using Microsoft.Build.Utilities;

namespace MSBuild.NuGet.Tasks.Util
{
    public static class XmlDocumentExtensions
    {
        /// <summary>
        /// Updates the node.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="log">The log.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="xpath">The xpath.</param>
        /// <param name="value">The value.</param>
        internal static void UpdateNode(this XmlDocument doc, TaskLoggingHelper log, XmlNamespaceManager manager, string xpath, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            log.LogMessage("Updating node " + xpath + " to value " + value);

            var node = doc.SelectSingleNode(xpath, manager);
            if (node != null)
            {
                node.InnerText = value;
            }
            else
            {
                log.LogMessage("Node " + xpath + " not found");
            }
        }

        /// <summary>
        /// Updates the CData node.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="log">The log.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="xpath">The xpath.</param>
        /// <param name="value">The value.</param>
        internal static void UpdateCDataNode(this XmlDocument doc, TaskLoggingHelper log, XmlNamespaceManager manager, string xpath, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            log.LogMessage("Updating cdata node " + xpath + " to value " + value);

            var node = doc.SelectSingleNode(xpath, manager);
            if (node != null)
            {
                var cDataSection = doc.CreateCDataSection(value);
                node.InnerXml = cDataSection.OuterXml;
            }
            else
            {
                log.LogMessage("Node " + xpath + " not found");
            }
        }

        /// <summary>
        /// Updates the attribute.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="log">The log.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="xpath">The xpath.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        internal static void UpdateAttribute(this XmlDocument doc, TaskLoggingHelper log, XmlNamespaceManager manager, string xpath, string attribute, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            log.LogMessage("Updating attribute "+ attribute+ " at " + xpath + " to value " + value);

            var node = doc.SelectSingleNode(xpath, manager);
            if (node != null)
            {
                var attr = node.Attributes[attribute];
                if (attr != null)
                {
                    attr.Value = value;
                }
                else
                {
                    log.LogMessage("Attribute " + attribute + " not found");
                }
            }
            else
            {
                log.LogMessage("Node " + xpath + " not found");
            }
        }

        /// <summary>
        /// Creates the file node.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="log">The log.</param>
        /// <param name="xmlns">The XMLNS.</param>
        /// <param name="src">The src.</param>
        /// <param name="target">The target.</param>
        /// <param name="exclude">The exclude.</param>
        /// <returns></returns>
        internal static XmlNode CreateFileNode(this XmlDocument doc, TaskLoggingHelper log, string xmlns, string src, string target, string exclude = "")
        {
            var node = doc.CreateNode(XmlNodeType.Element, "file", xmlns);

            var srcAttr = doc.CreateAttribute("src");
            srcAttr.InnerText = src;
            node.Attributes.Append(srcAttr);

            var targetAttr = doc.CreateAttribute("target");
            targetAttr.InnerText = target;
            node.Attributes.Append(targetAttr);

            if(!string.IsNullOrWhiteSpace(exclude))
            {
                var excludeAttr = doc.CreateAttribute("exclude");
                excludeAttr.InnerText = exclude;
                node.Attributes.Append(excludeAttr);
            }

            return node;
        }
    }
}
