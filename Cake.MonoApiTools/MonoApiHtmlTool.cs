using Cake.Core.Tooling;
using Cake.Core.IO;
using Cake.Core;
using System.Collections.Generic;

namespace Cake.MonoApiTools
{
    /// <summary>
    /// Tool settings for mono-api-html
    /// </summary>
    public class MonoApiHtmlToolSettings : ToolSettings
    {
    }

    class MonoApiHtmlTool : Tool<MonoApiHtmlToolSettings>
    {
        public MonoApiHtmlTool (ICakeContext cakeContext, IFileSystem fileSystem, ICakeEnvironment cakeEnvironment, IProcessRunner processRunner, IToolLocator toolLocator)
            : base (fileSystem, cakeEnvironment, processRunner, toolLocator)
        {
            environment = cakeEnvironment;
        }

        ICakeEnvironment environment;

        protected override string GetToolName ()
        {
            return "mono-api-html";
        }

        protected override IEnumerable<string> GetToolExecutableNames ()
        {
            return new List<string> {
                "mono-api-html.exe"
            };
        }

        // Now let's make a purty html file
        // eg: mono mono-api-html.exe -c -x ./gps.previous.info.xml ./gps.current.info.xml > gps.diff.html    
        public IEnumerable<string> ApiHtml (FilePath previousApiInfo, FilePath newApiInfo, MonoApiHtmlToolSettings settings = null)
        {
            //Arguments = "-c -x ./output/GooglePlayServices.api-info.previous.xml ./output/GooglePlayServices.api-info.xml",
            var builder = new ProcessArgumentBuilder ();
            builder.Append ("-c");
            builder.Append ("-x");
            builder.AppendQuoted (previousApiInfo.MakeAbsolute (environment).FullPath);
            builder.AppendQuoted (newApiInfo.MakeAbsolute (environment).FullPath);

            var process = RunProcess (settings ?? new MonoApiHtmlToolSettings (), builder);

            process.WaitForExit ();

            return process.GetStandardOutput ();
        }

        public void ApiHtml (FilePath previousApiInfo, FilePath newApiInfo, FilePath outputFile, MonoApiHtmlToolSettings settings = null)
        {
            var builder = new ProcessArgumentBuilder ();
            builder.Append ("-c");
            builder.Append ("-x");
            builder.AppendQuoted (previousApiInfo.MakeAbsolute (environment).FullPath);
            builder.AppendQuoted (newApiInfo.MakeAbsolute (environment).FullPath);

            var process = RunProcess (settings ?? new MonoApiHtmlToolSettings (), builder);

            process.WaitForExit ();

            System.IO.File.WriteAllLines (
                    outputFile.MakeAbsolute (environment).FullPath,
                    process.GetStandardOutput ());
        }
    }
}

