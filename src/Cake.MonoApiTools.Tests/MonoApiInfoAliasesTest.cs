using Cake.Core.IO;
using Cake.Testing;
using System;
using Xunit;

namespace Cake.MonoApiTools.Tests
{
    public class MonoApiInfoAliasesTest
    {
        [Theory]
        [InlineData("/bin/tools/mono-api-info.exe", "/bin/tools/mono-api-info.exe")]
        [InlineData("./tools/mono-api-info.exe", "/Working/tools/mono-api-info.exe")]
        public void Should_Use_Executable_From_Tool_Path_If_Provided(string toolPath, string expected)
        {
            // Given
            var fixture = new MonoApiInfoFixture();
            fixture.Settings.ToolPath = toolPath;
            fixture.GivenSettingsToolPathExist();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal(expected, result.Path.FullPath);
        }

        [Fact]
        public void Should_Throw_If_Input_File_Is_Null()
        {
            // Given
            var fixture = new MonoApiInfoFixture();
            fixture.AssemblyPaths = null;

            // When + Then
            var result = Assert.Throws<ArgumentNullException>("assemblies", () => fixture.Run());
        }
        [Fact]
        public void Should_Throw_If_Input_Files_Are_Empty()
        {
            // Given
            var fixture = new MonoApiInfoFixture();
            fixture.AssemblyPaths = new FilePath[0];

            // When + Then
            var result = Assert.Throws<ArgumentException>("assemblies", () => fixture.Run());
        }

        [Fact]
        public void Should_Find_Executable_If_Tool_Path_Was_Not_Provided()
        {
            // Given
            var fixture = new MonoApiInfoFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working/tools/mono-api-info.exe", result.Path.FullPath);
        }

        [Fact]

        public void Should_Throw_When_Only_Assembly_Path()
        {
            // Given
            var fixture = new MonoApiInfoFixture();
            fixture.OutputPath = null;

            // When + Then
            var result = Assert.Throws<ArgumentNullException>("outputPath", () => fixture.Run());
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Assembly_And_Output()
        {
            // Given
            var fixture = new MonoApiInfoFixture();

            // When
            var result = fixture.Run();

            // Then
            var args =
                "-o=\"/Working/input.info.xml\" " +
                "\"/Working/input.dll\"";
            Assert.Equal(args, result.Args);
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Abi()
        {
            // Given
            var fixture = new MonoApiInfoFixture();
            fixture.Settings.GenerateAbi = true;

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--abi " +
                "-o=\"/Working/input.info.xml\" " +
                "\"/Working/input.dll\"";
            Assert.Equal(args, result.Args);
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Everything()
        {
            // Given
            var fixture = new MonoApiInfoFixture();
            fixture.Settings.GenerateAbi = true;
            fixture.Settings.FollowForwarders = true;
            fixture.Settings.GenerateContractApi = true;
            fixture.Settings.SearchPaths = new[]
            {
                new DirectoryPath("/search/path/a"),
                new DirectoryPath("/search/path/b")
            };
            fixture.Settings.ResolvePaths = new[]
            {
                new FilePath("/resolve/assembly/a.dll"),
                new FilePath("/resolve/assembly/b.dll"),
            };

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--abi " +
                "--follow-forwarders " +
                "--search-directory=\"/search/path/a\" " +
                "--search-directory=\"/search/path/b\" " +
                "-r=\"/resolve/assembly/a.dll\" " +
                "-r=\"/resolve/assembly/b.dll\" " +
                "-o=\"/Working/input.info.xml\" " +
                "--contract-api " +
                "\"/Working/input.dll\"";
            Assert.Equal(args, result.Args);
        }
    }
}
