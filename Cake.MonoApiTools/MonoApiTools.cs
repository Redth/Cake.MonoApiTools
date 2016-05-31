using Cake.Core.Annotations;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.MonoApiTools
{
    /// <summary>
    /// File helper aliases.
    /// </summary>
    [CakeAliasCategory("Mono API Tools")]
    public static class MonoApiToolsAliases
    {
        /// <summary>
        /// Runs mono-api-diff
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        [CakeMethodAlias]
        public static void MonoApiDiff (this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile)
        {
            MonoApiDiff (context, previousApiInfoFile, newApiInfoFile, outputFile, null);
        }

        /// <summary>
        /// Runs mono-api-diff
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        /// <param name="settings">The tool settings.</param>
        [CakeMethodAlias]
        public static void MonoApiDiff (this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile, MonoApiTools.MonoApiDiffToolSettings settings)
        {
            var tool = new MonoApiTools.MonoApiDiffTool (context, context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            tool.ApiInfoDiff (previousApiInfoFile, newApiInfoFile, outputFile, settings);
        }

        /// <summary>
        /// Runs mono-api-info
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dotNetAssembly">The .NET assembly to generate API info xml from.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        [CakeMethodAlias]
        public static void MonoApiInfo (this ICakeContext context, FilePath dotNetAssembly, FilePath outputFile)
        {
            MonoApiInfo (context, dotNetAssembly, outputFile, null);
        }

        /// <summary>
        /// Runs mono-api-info
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dotNetAssembly">The .NET assembly to generate API info xml from.</param>
        /// <param name="outputFile">The API Info xml output file.</param>
        /// <param name="settings">The tool settings.</param>
        [CakeMethodAlias]
        public static void MonoApiInfo (this ICakeContext context, FilePath dotNetAssembly, FilePath outputFile, MonoApiTools.MonoApiInfoToolSettings settings)
        {
            var tool = new MonoApiTools.MonoApiInfoTool (context, context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            tool.ApiInfo (dotNetAssembly, outputFile, settings);
        }

        /// <summary>
        /// Runs mono-api-html to generate an HTML diff
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Diff Html output file.</param>
        [CakeMethodAlias]
        public static void MonoApiHtml (this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile)
        {
            MonoApiHtml (context, previousApiInfoFile, newApiInfoFile, outputFile, null);
        }

        /// <summary>
        /// Runs mono-api-html to generate an HTML diff
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="previousApiInfoFile">The first API-Info xml file to compare.</param>
        /// <param name="newApiInfoFile">The second API Info xml file to compare.</param>
        /// <param name="outputFile">The API Diff Html output file.</param>
        /// <param name="settings">The tool settings.</param>
        [CakeMethodAlias]
        public static void MonoApiHtml (this ICakeContext context, FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile, MonoApiTools.MonoApiHtmlToolSettings settings)
        {
            var tool = new MonoApiTools.MonoApiHtmlTool (context, context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            tool.ApiHtml (previousApiInfoFile, newApiInfoFile, settings);
        }
    }
}

