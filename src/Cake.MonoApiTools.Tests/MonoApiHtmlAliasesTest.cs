using Cake.Testing;
using System;
using Xunit;

namespace Cake.MonoApiTools.Tests
{
    public class MonoApiHtmlAliasesTest
    {
        [Theory]
        [InlineData("/bin/tools/mono-api-html.exe", "/bin/tools/mono-api-html.exe")]
        [InlineData("./tools/mono-api-html.exe", "/Working/tools/mono-api-html.exe")]
        public void Should_Use_Executable_From_Tool_Path_If_Provided(string toolPath, string expected)
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
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
            var fixture = new MonoApiHtmlFixture();
            fixture.FirstInfo = null;

            // When + Then
            var result = Assert.Throws<ArgumentNullException>("firstInfo", () => fixture.Run());
        }

        [Fact]
        public void Should_Throw_If_Second_Input_File_Is_Null()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
            fixture.SecondInfo = null;

            // When + Then
            var result = Assert.Throws<ArgumentNullException>("secondInfo", () => fixture.Run());
        }

        [Fact]
        public void Should_Find_Executable_If_Tool_Path_Was_Not_Provided()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working/tools/mono-api-html.exe", result.Path.FullPath);
        }

        [Fact]

        public void Should_Throw_When_Only_Info_Paths()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
            fixture.OutputPath = null;

            // When + Then
            var result = Assert.Throws<ArgumentNullException>("outputPath", () => fixture.Run());
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Info_And_Output()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--diff=\"/Working/diff.html\" " +
                "\"/Working/version-one.xml\" " +
                "\"/Working/version-two.xml\"";
            Assert.Equal(args, result.Args);
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Markdown()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
            fixture.OutputPath = "diff.md";
            fixture.Settings.OutputFormat = MonoApiHtmlOutputFormat.Markdown;

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--markdown " +
                "--diff=\"/Working/diff.md\" " +
                "\"/Working/version-one.xml\" " +
                "\"/Working/version-two.xml\"";
            Assert.Equal(args, result.Args);
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Html()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
            fixture.Settings.OutputFormat = MonoApiHtmlOutputFormat.Html;

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--diff=\"/Working/diff.html\" " +
                "\"/Working/version-one.xml\" " +
                "\"/Working/version-two.xml\"";
            Assert.Equal(args, result.Args);
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Colorize()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
            fixture.Settings.Colorize = true;

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--colorize=true " +
                "--diff=\"/Working/diff.html\" " +
                "\"/Working/version-one.xml\" " +
                "\"/Working/version-two.xml\"";
            Assert.Equal(args, result.Args);
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Single_Ignore()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
            fixture.Settings.Ignore = new[]
            {
                "INSCopying$"
            };

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--ignore=\"INSCopying$\" " +
                "--diff=\"/Working/diff.html\" " +
                "\"/Working/version-one.xml\" " +
                "\"/Working/version-two.xml\"";
            Assert.Equal(args, result.Args);
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Multiple_Ignore()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
            fixture.Settings.Ignore = new[]
            {
                "INSCopying$",
                "INSCoding$"
            };

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--ignore=\"INSCopying$\" " +
                "--ignore=\"INSCoding$\" " +
                "--diff=\"/Working/diff.html\" " +
                "\"/Working/version-one.xml\" " +
                "\"/Working/version-two.xml\"";
            Assert.Equal(args, result.Args);
        }

        [Fact]
        public void Should_Create_Correct_Command_Line_Arguments_For_Specific_Ignore()
        {
            // Given
            var fixture = new MonoApiHtmlFixture();
            fixture.Settings.IgnoreAdded = new[] { "a", "b" };
            fixture.Settings.IgnoreRemoved = new[] { "c", "d" };
            fixture.Settings.IgnoreNew = new[] { "e", "f" };

            // When
            var result = fixture.Run();

            // Then
            var args =
                "--ignore-added=\"a\" --ignore-added=\"b\" " +
                "--ignore-removed=\"c\" --ignore-removed=\"d\" " +
                "--ignore-new=\"e\" --ignore-new=\"f\" " +
                "--diff=\"/Working/diff.html\" " +
                "\"/Working/version-one.xml\" " +
                "\"/Working/version-two.xml\"";
            Assert.Equal(args, result.Args);
        }
    }
}
