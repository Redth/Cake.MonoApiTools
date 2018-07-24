using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.MonoApiTools.Tests
{
    internal sealed class MonoApiHtmlFixture : ToolFixture<MonoApiHtmlToolSettings>
    {
        public MonoApiHtmlFixture()
            : base("mono-api-html.exe")
        {
            FirstInfo = "version-one.xml";
            SecondInfo = "version-two.xml";
            OutputPath = "diff.html";
        }

        public FilePath FirstInfo { get; set; }

        public FilePath SecondInfo { get; set; }

        public FilePath OutputPath { get; set; }

        protected override void RunTool()
        {
            var tool = new MonoApiHtmlTool(FileSystem, Environment, ProcessRunner, Tools);

            tool.Execute(FirstInfo, SecondInfo, OutputPath, Settings);
        }
    }
}
