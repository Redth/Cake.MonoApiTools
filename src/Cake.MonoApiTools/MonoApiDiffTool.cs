using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MonoApiTools
{
    public sealed class MonoApiDiffTool : Tool<MonoApiDiffToolSettings>
    {
        private ICakeEnvironment environment;

        public MonoApiDiffTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            this.environment = environment;
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "mono-api-diff.exe" };
        }

        protected override string GetToolName()
        {
            return "mono-api-diff";
        }

        public void Execute(FilePath firstInfo, FilePath secondInfo, FilePath outputPath, MonoApiDiffToolSettings settings)
        {
            if (firstInfo == null)
                throw new ArgumentNullException(nameof(firstInfo));
            if (secondInfo == null)
                throw new ArgumentNullException(nameof(secondInfo));
            if (outputPath == null)
                throw new ArgumentNullException(nameof(outputPath));

            settings = settings ?? new MonoApiDiffToolSettings();

            Run(settings, GetArguments(firstInfo, secondInfo, outputPath, settings));
        }

        private ProcessArgumentBuilder GetArguments(FilePath firstInfo, FilePath secondInfo, FilePath outputPath, MonoApiDiffToolSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            builder.AppendSwitchQuoted("--output", "=", outputPath.MakeAbsolute(environment).FullPath);

            builder.AppendQuoted(firstInfo.MakeAbsolute(environment).FullPath);

            builder.AppendQuoted(secondInfo.MakeAbsolute(environment).FullPath);

            return builder;
        }
    }
}
