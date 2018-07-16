using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.MonoApiTools
{
    /// <summary>
    /// Tool settings for mono-api-info.
    /// </summary>
    public class MonoApiInfoToolSettings : ToolSettings
    {
        public bool GenerateAbi { get; set; }

        public bool GenerateContractApi { get; set; }

        public bool FollowForwarders { get; set; }

        /// <summary>
        /// Gets or sets the paths to search for referenced assemblies.
        /// </summary>
        public DirectoryPath[] SearchPaths { get; set; }

        public FilePath[] ResolvePaths { get; set; }
    }
}
