using Cake.Core.Tooling;

namespace Cake.MonoApiTools
{
    /// <summary>
    /// Various output formats for mono-api-html
    /// </summary>
    public enum MonoApiHtmlOutputFormat
    {
        /// <summary>
        /// Outputs HTML.
        /// </summary>
        Html,

        /// <summary>
        /// Outputs Markdown.
        /// </summary>
        Markdown
    }

    /// <summary>
    /// Tool settings for mono-api-html.
    /// </summary>
    public class MonoApiHtmlToolSettings : ToolSettings
    {
        public string[] Ignore { get; set; }

        public string[] IgnoreAdded { get; set; }

        public string[] IgnoreRemoved { get; set; }

        public string[] IgnoreNew { get; set; }

        public bool IgnoreChangedParameterNames { get; set; }

        public bool IgnoreChangedPropertySetters { get; set; }

        public bool IgnoreChangedVirtual { get; set; }

        public bool IgnoreNonBreaking { get; set; }

        public bool IgnoreDuplicateXml { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output should be colorized.
        /// </summary>
        public bool Colorize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to log additional information.
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating what the format of the output file should be.
        /// </summary>
        public MonoApiHtmlOutputFormat OutputFormat { get; set; }
    }
}
