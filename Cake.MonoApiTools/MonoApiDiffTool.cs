using Cake.Core.Tooling;
using Cake.Core.IO;
using Cake.Core;
using System.Collections.Generic;

namespace Cake.MonoApiTools
{
    /// <summary>
    /// Tool settings for mono-api-diff
    /// </summary>
    public class MonoApiDiffToolSettings : ToolSettings
    {
    }

    class MonoApiDiffTool : Tool<MonoApiDiffToolSettings>
    {
        public MonoApiDiffTool (ICakeContext cakeContext, IFileSystem fileSystem, ICakeEnvironment cakeEnvironment, IProcessRunner processRunner, IToolLocator toolLocator)
            : base (fileSystem, cakeEnvironment, processRunner, toolLocator)
        {
            environment = cakeEnvironment;
        }

        ICakeEnvironment environment;

        protected override string GetToolName ()
        {
            return "mono-api-diff";
        }

        protected override IEnumerable<string> GetToolExecutableNames ()
        {
            return new List<string> {
                "mono-api-diff.exe"
            };
        }

        // Now diff against current release'd api info
        // eg: mono mono-api-diff.exe ./gps.r26.xml ./gps.r27.xml > gps.diff.xml    
        public IEnumerable<string> ApiInfoDiff (FilePath previousApiInfo, FilePath newApiInfo, MonoApiDiffToolSettings settings = null)
        {
            var builder = new ProcessArgumentBuilder ();
            //android list sdk --all 
            builder.AppendQuoted (previousApiInfo.MakeAbsolute (environment).FullPath);
            builder.AppendQuoted (newApiInfo.MakeAbsolute (environment).FullPath);

            var process = RunProcess (settings ?? new MonoApiDiffToolSettings (), builder);

            process.WaitForExit ();

            return process.GetStandardOutput ();
        }

        public void ApiInfoDiff (FilePath previousApiInfo, FilePath newApiInfo, FilePath outputFile, MonoApiDiffToolSettings settings = null)
        {
            var builder = new ProcessArgumentBuilder ();
            //android list sdk --all 
            builder.AppendQuoted (previousApiInfo.MakeAbsolute (environment).FullPath);
            builder.AppendQuoted (newApiInfo.MakeAbsolute (environment).FullPath);

            var process = RunProcess (settings ?? new MonoApiDiffToolSettings (), builder);

            process.WaitForExit ();

            System.IO.File.WriteAllLines (
                    outputFile.MakeAbsolute (environment).FullPath,
                    process.GetStandardOutput ());
        }
    }
}

