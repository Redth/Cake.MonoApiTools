using Cake.Testing;
using System;
using Xunit;

namespace Cake.MonoApiTools.Tests
{
    public class MonoApiDiffAliasesTest
    {
        [Theory]
        [InlineData("/bin/tools/mono-api-diff.exe", "/bin/tools/mono-api-diff.exe")]
        [InlineData("./tools/mono-api-diff.exe", "/Working/tools/mono-api-diff.exe")]
        public void Should_Use_Executable_From_Tool_Path_If_Provided(string toolPath, string expected)
        {
            // Given
            var fixture = new MonoApiDiffFixture();
            fixture.Settings.ToolPath = toolPath;
            fixture.GivenSettingsToolPathExist();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal(expected, result.Path.FullPath);
        }

        [Fact]
        public void Should_Throw_If_First_Input_File_Is_Null()
        {
            // Given
            var fixture = new MonoApiDiffFixture();
            fixture.FirstInfo = null;

            // When + Then
            var result = Assert.Throws<ArgumentNullException>("firstInfo", () => fixture.Run());
        }

        [Fact]
        public void Should_Throw_If_Second_Input_File_Is_Null()
        {
            // Given
            var fixture = new MonoApiDiffFixture();
            fixture.SecondInfo = null;

            // When + Then
            var result = Assert.Throws<ArgumentNullException>("secondInfo", () => fixture.Run());
        }

        [Fact]
        public void Should_Find_Executable_If_Tool_Path_Was_Not_Provided()
        {
            // Given
            var fixture = new MonoApiDiffFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working/tools/mono-api-diff.exe", result.Path.FullPath);
        }

        [Fact]

        public void Should_Throw_When_Only_Info_Paths()
        {
            // Given
            var fixture = new MonoApiDiffFixture();
            fixture.OutputPath = null;

            // When + Then
            var result = Assert.Throws<ArgumentNullException>("outputPath", () => fixture.Run());
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Info_And_Output()
        {
            // Given
            var fixture = new MonoApiDiffFixture();

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--output=\"/Working/diff.xml\" " +
                "\"/Working/version-one.xml\" " +
                "\"/Working/version-two.xml\"";
            Assert.Equal(args, result.Args);
        }
    }
}
