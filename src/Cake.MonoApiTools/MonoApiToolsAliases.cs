using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.MonoApiTools
{
    /// <summary>
    /// Contains functionality to work with the Mono API Tools.
    /// </summary>
    [CakeAliasCategory("Mono API Tools")]
    public static class MonoApiToolsAliases
    {
        // MonoApiInfo

        /// <summary>
        /// Runs mono-api-info.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dotNetAssembly">The .NET assembly to generate API info xml from.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        [CakeMethodAlias]
        public static void MonoApiInfo(this ICakeContext context, FilePath dotNetAssembly, FilePath outputFile)
        {
            if (dotNetAssembly == null)
                throw new ArgumentNullException(nameof(dotNetAssembly));

            MonoApiInfo(context, new[] { dotNetAssembly }, outputFile, null);
        }

        /// <summary>
        /// Runs mono-api-info.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dotNetAssembly">The .NET assembly to generate API info xml from.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        /// <param name="settings">The tool settings.</param>
        [CakeMethodAlias]
        public static void MonoApiInfo(this ICakeContext context, FilePath dotNetAssembly, FilePath outputFile, MonoApiInfoToolSettings settings)
        {
            if (dotNetAssembly == null)
                throw new ArgumentNullException(nameof(dotNetAssembly));

            MonoApiInfo(context, new[] { dotNetAssembly }, outputFile, settings);
        }

        /// <summary>
        /// Runs mono-api-info.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="assemblies">The .NET assemblies to generate API info xml from.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        [CakeMethodAlias]
        public static void MonoApiInfo(this ICakeContext context, FilePath[] assemblies, FilePath outputFile)
        {
            MonoApiInfo(context, assemblies, outputFile, null);
        }

        /// <summary>
        /// Runs mono-api-info.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="assemblies">The .NET assemblies to generate API info xml from.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        /// <param name="settings">The tool settings.</param>
        [CakeMethodAlias]
        public static void MonoApiInfo(this ICakeContext context, FilePath[] assemblies, FilePath outputFile, MonoApiInfoToolSettings settings)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));
            if (outputFile == null)
                throw new ArgumentNullException(nameof(outputFile));

            var tool = new MonoApiInfoTool(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            tool.Execute(assemblies, outputFile, settings);
        }


        // MonoApiDiff

        /// <summary>
        /// Runs mono-api-diff to generate an XML diff.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        [CakeMethodAlias]
        public static void MonoApiDiff(this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile)
        {
            MonoApiDiff(context, previousApiInfoFile, newApiInfoFile, outputFile, null);
        }

        /// <summary>
        /// Runs mono-api-diff to generate an XML diff.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        /// <param name="settings">The tool settings.</param>
        [CakeMethodAlias]
        public static void MonoApiDiff(this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile, MonoApiDiffToolSettings settings)
        {
            if (previousApiInfoFile == null)
                throw new ArgumentNullException(nameof(previousApiInfoFile));
            if (newApiInfoFile == null)
                throw new ArgumentNullException(nameof(newApiInfoFile));
            if (outputFile == null)
                throw new ArgumentNullException(nameof(outputFile));

            var tool = new MonoApiDiffTool(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            tool.Execute(previousApiInfoFile, newApiInfoFile, outputFile, settings);
        }


        // MonoApiHtml

        /// <summary>
        /// Runs mono-api-html to generate an HTML diff.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Diff Html output file.</param>
        [CakeMethodAlias]
        public static void MonoApiHtml(this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile)
        {
            MonoApiHtml(context, previousApiInfoFile, newApiInfoFile, outputFile, null);
        }

        /// <summary>
        /// Runs mono-api-html to generate an HTML diff.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Diff Html output file.</param>
        /// <param name="settings">The tool settings.</param>
        [CakeMethodAlias]
        public static void MonoApiHtml(this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile, MonoApiHtmlToolSettings settings)
        {
            if (previousApiInfoFile == null)
                throw new ArgumentNullException(nameof(previousApiInfoFile));
            if (newApiInfoFile == null)
                throw new ArgumentNullException(nameof(newApiInfoFile));
            if (outputFile == null)
                throw new ArgumentNullException(nameof(outputFile));

            var tool = new MonoApiHtmlTool(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            tool.Execute(previousApiInfoFile, newApiInfoFile, outputFile, settings);
        }

        /// <summary>
        /// Runs mono-api-html to generate an colorized HTML diff.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Diff Html output file.</param>
        [CakeMethodAlias]
        public static void MonoApiHtmlColorized(this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile)
        {
            MonoApiHtml(context, previousApiInfoFile, newApiInfoFile, outputFile, new MonoApiHtmlToolSettings
            {
                Colorize = true
            });
        }

        /// <summary>
        /// Runs mono-api-html to generate an Markdown diff.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Diff Html output file.</param>
        [CakeMethodAlias]
        public static void MonoApiMarkdown(this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile)
        {
            MonoApiHtml(context, previousApiInfoFile, newApiInfoFile, outputFile, new MonoApiHtmlToolSettings
            {
                OutputFormat = MonoApiHtmlOutputFormat.Markdown
            });
        }

        /// <summary>
        /// Runs mono-api-html to generate an colorized Markdown diff.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Diff Html output file.</param>
        [CakeMethodAlias]
        public static void MonoApiMarkdownColorized(this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile)
        {
            MonoApiHtml(context, previousApiInfoFile, newApiInfoFile, outputFile, new MonoApiHtmlToolSettings
            {
                Colorize = true,
                OutputFormat = MonoApiHtmlOutputFormat.Markdown
            });
        }
    }
}
