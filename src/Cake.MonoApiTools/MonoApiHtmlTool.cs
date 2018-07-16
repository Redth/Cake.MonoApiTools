using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MonoApiTools
{
    public sealed class MonoApiHtmlTool : Tool<MonoApiHtmlToolSettings>
    {
        private ICakeEnvironment environment;

        public MonoApiHtmlTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            this.environment = environment;
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "mono-api-html.exe" };
        }

        protected override string GetToolName()
        {
            return "mono-api-html";
        }

        public void Execute(FilePath firstInfo, FilePath secondInfo, FilePath outputPath, MonoApiHtmlToolSettings settings)
        {
            if (firstInfo == null)
                throw new ArgumentNullException(nameof(firstInfo));
            if (secondInfo == null)
                throw new ArgumentNullException(nameof(secondInfo));
            if (outputPath == null)
                throw new ArgumentNullException(nameof(outputPath));

            settings = settings ?? new MonoApiHtmlToolSettings();

            Run(settings, GetArguments(firstInfo, secondInfo, outputPath, settings));
        }

        private ProcessArgumentBuilder GetArguments(FilePath firstInfo, FilePath secondInfo, FilePath outputPath, MonoApiHtmlToolSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            AddIgnores("--ignore", settings.Ignore);

            AddIgnores("--ignore-added", settings.IgnoreAdded);

            AddIgnores("--ignore-removed", settings.IgnoreRemoved);

            AddIgnores("--ignore-new", settings.IgnoreNew);

            if (settings.IgnoreChangedParameterNames)
                builder.Append("--ignore-changes-parameter-names");

            if (settings.IgnoreChangedPropertySetters)
                builder.Append("--ignore-changes-property-setters");

            if (settings.IgnoreChangedVirtual)
                builder.Append("--ignore-changes-virtual");

            if (settings.IgnoreNonBreaking)
                builder.Append("--ignore-nonbreaking");

            if (settings.IgnoreDuplicateXml)
                builder.Append("--lax");

            if (settings.Colorize)
                builder.AppendSwitch("--colorize", "=", "true");

            if (settings.Verbose)
                builder.Append("--verbose");

            switch (settings.OutputFormat)
            {
                case MonoApiHtmlOutputFormat.Markdown:
                    builder.Append("--markdown");
                    break;
                case MonoApiHtmlOutputFormat.Html:
                default:
                    // the default is HTML
                    break;
            }

            builder.AppendSwitchQuoted("--diff", "=", outputPath.MakeAbsolute(environment).FullPath);

            builder.AppendQuoted(firstInfo.MakeAbsolute(environment).FullPath);

            builder.AppendQuoted(secondInfo.MakeAbsolute(environment).FullPath);

            return builder;

            void AddIgnores(string name, string[] ignores)
            {
                if (ignores?.Length > 0)
                {
                    foreach (var ignore in ignores)
                    {
                        builder.AppendSwitchQuoted(name, "=", ignore);
                    }
                }
            }
        }
    }
}
