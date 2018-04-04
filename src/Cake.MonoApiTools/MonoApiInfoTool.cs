using Cake.Core.Tooling;
using Cake.Core.IO;
using Cake.Core;
using System.Collections.Generic;

namespace Cake.MonoApiTools
{
    /// <summary>
    /// Tool settings for mono-api-info
    /// </summary>
    public class MonoApiInfoToolSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets the paths to search for referenced assemblies.
        /// </summary>
        /// <value>The search paths.</value>
        public FilePath [] SearchPaths { get; set; }
    }

    class MonoApiInfoTool : Tool<MonoApiInfoToolSettings>
    {
        public MonoApiInfoTool (ICakeContext cakeContext, IFileSystem fileSystem, ICakeEnvironment cakeEnvironment, IProcessRunner processRunner, IToolLocator toolLocator)
            : base (fileSystem, cakeEnvironment, processRunner, toolLocator)
        {
            environment = cakeEnvironment;
        }

        ICakeEnvironment environment;

        protected override string GetToolName ()
        {
            return "mono-api-info";
        }

        protected override IEnumerable<string> GetToolExecutableNames ()
        {
            return new List<string> {
                "mono-api-info.exe"
            };
        }

        //eg: mono mono-api-info.exe --search-directory=/Library/Frameworks/Xamarin.Android.framework/Libraries/mandroid/platforms/android-23 ./Xamarin.GooglePlayServices.r27.dll > gps.r27.xml
        public IEnumerable<string> ApiInfo (FilePath assembly, MonoApiInfoToolSettings settings = null)
        {
            var builder = new ProcessArgumentBuilder ();

            if (settings != null && settings.SearchPaths != null) {
                foreach (var p in settings.SearchPaths)
                    builder.Append ("--search-directory=\"{0}\"", p.MakeAbsolute (environment));
            }
            
            builder.AppendQuoted (assembly.MakeAbsolute (environment).FullPath);

            var process = RunProcess (settings ?? new MonoApiInfoToolSettings (), 
                                      builder, 
                                      new ProcessSettings { RedirectStandardOutput = true });

            process.WaitForExit ();

            return process.GetStandardOutput ();
        }

        public void ApiInfo (FilePath assembly, FilePath outputFile, MonoApiInfoToolSettings settings = null)
        {
            var builder = new ProcessArgumentBuilder ();

            if (settings != null && settings.SearchPaths != null) {
                foreach (var p in settings.SearchPaths)
                    builder.Append ("--search-directory=\"{0}\"", p.MakeAbsolute (environment));
            }

            builder.AppendQuoted (assembly.MakeAbsolute (environment).FullPath);

            var process = RunProcess (settings ?? new MonoApiInfoToolSettings (), 
                                      builder,
                                      new ProcessSettings { RedirectStandardOutput = true });

            process.WaitForExit ();

            System.IO.File.WriteAllLines (
                    outputFile.MakeAbsolute (environment).FullPath,
                    process.GetStandardOutput ());
        }
    }
}

