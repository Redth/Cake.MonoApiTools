using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.MonoApiTools.Tests
{
    internal sealed class MonoApiInfoFixture : ToolFixture<MonoApiInfoToolSettings>
    {
        public MonoApiInfoFixture()
            : base("mono-api-info.exe")
        {
            AssemblyPaths = new[] { (FilePath)"input.dll" };
            OutputPath = "input.info.xml";
        }

        public FilePath[] AssemblyPaths { get; set; }

        public FilePath OutputPath { get; set; }

        protected override void RunTool()
        {
            var tool = new MonoApiInfoTool(FileSystem, Environment, ProcessRunner, Tools);

            tool.Execute(AssemblyPaths, OutputPath, Settings);
        }
    }
}
