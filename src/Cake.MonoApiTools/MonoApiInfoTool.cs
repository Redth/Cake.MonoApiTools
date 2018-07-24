using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MonoApiTools
{
    public sealed class MonoApiInfoTool : Tool<MonoApiInfoToolSettings>
    {
        private ICakeEnvironment environment;

        public MonoApiInfoTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            this.environment = environment;
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "mono-api-info.exe" };
        }

        protected override string GetToolName()
        {
            return "mono-api-info";
        }

        public void Execute(FilePath[] assemblies, FilePath outputPath, MonoApiInfoToolSettings settings)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));
            if (assemblies.Length == 0)
                throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
            if (outputPath == null)
                throw new ArgumentNullException(nameof(outputPath));

            settings = settings ?? new MonoApiInfoToolSettings();

            Run(settings, GetArguments(assemblies, outputPath, settings));
        }

        private ProcessArgumentBuilder GetArguments(FilePath[] assemblies, FilePath outputPath, MonoApiInfoToolSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            if (settings.GenerateAbi)
                builder.Append("--abi");

            if (settings.FollowForwarders)
                builder.Append("--follow-forwarders");

            if (settings.SearchPaths?.Length > 0)
            {
                foreach (var path in settings.SearchPaths)
                {
                    builder.AppendSwitchQuoted("--search-directory", "=", path.MakeAbsolute(environment).FullPath);
                }
            }

            if (settings.ResolvePaths?.Length > 0)
            {
                foreach (var path in settings.ResolvePaths)
                {
                    builder.AppendSwitchQuoted("-r", "=", path.MakeAbsolute(environment).FullPath);
                }
            }

            if (outputPath != null)
                builder.AppendSwitchQuoted("-o", "=", outputPath.MakeAbsolute(environment).FullPath);

            if (settings.GenerateContractApi)
                builder.Append("--contract-api");

            foreach (var assembly in assemblies)
            {
                builder.AppendQuoted(assembly.MakeAbsolute(environment).FullPath);
            }

            return builder;
        }
    }
}
