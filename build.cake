#addin "Cake.FileHelpers"

#tool xunit.runner.console&version=2.4.0-rc.2.build4045

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// a bit of logic to create the version number:
//  - input                     = 1.2.3.4
//  - package version           = 1.2.3
//  - preview package version   = 1.2.3-preview4
var version = Version.Parse(Argument("packageversion", EnvironmentVariable("APPVEYOR_BUILD_VERSION") ?? "1.0.0.0"));
var previewNumber   = version.Revision;
var assemblyVersion = $"{version.Major}.0.0.0";
var fileVersion     = $"{version.Major}.{version.Minor}.{version.Build}.0";
var infoVersion     = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
var packageVersion  = $"{version.Major}.{version.Minor}.{version.Build}";

// TODO: waiting on this package to be merged and released
var nugetUrl = "https://jenkins.mono-project.com/job/Components-OpenSource-PR/35/Azure/processDownloadRequest/ArtifactsFor-35/a27ca016cfba97352e948ceaf48badccf96cbc9f/XPlat/Mono.ApiTools/output/Mono.ApiTools.5.12.0.273-preview.nupkg";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Externals")
    .Does (() =>
{
    Information("Downloading the mono-api-tools package...");

    var dest = "./externals/Mono.ApiTools.nupkg";

    if (!FileExists(dest)) {
        EnsureDirectoryExists("./externals/");
        CleanDirectories("./externals/");
        DownloadFile(nugetUrl, dest);
        Unzip(dest, "./externals/");
    }

    Information("Download complete.");
});

Task("Build")
    .IsDependentOn("Externals")
    .Does (() =>
{
    var sln = "./src/Cake.MonoApiTools.sln";

    Information("Building {0}...", sln);

    var settings = new MSBuildSettings()
        .SetConfiguration(configuration)
        .WithRestore()
        .WithProperty("NoWarn", "1591") // ignore missing XML doc warnings
        .WithProperty("TreatWarningsAsErrors", "True")
        .WithProperty("Version", assemblyVersion)
        .WithProperty("FileVersion", fileVersion)
        .WithProperty("InformationalVersion", infoVersion)
        .SetVerbosity(Verbosity.Minimal)
        .SetNodeReuse(false);

    MSBuild(sln, settings);

    Information("Build complete.");
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var test = $"./src/Cake.MonoApiTools.Tests/bin/{configuration}/net462/Cake.MonoApiTools.Tests.dll";

    Information("Testing {0}...", test);

    XUnit2(test, new XUnit2Settings {
        OutputDirectory = "./output/",
        XmlReport = true
    });

    Information("Test complete.");
});

Task("Package")
    .IsDependentOn("Build")
    .Does (() =>
{
    var proj = "./src/Cake.MonoApiTools/Cake.MonoApiTools.csproj";

    Information("Packing {0}...", proj);

    var settings = new MSBuildSettings()
        .SetConfiguration(configuration)
        .WithTarget("Pack")
        .WithProperty("IncludeSymbols", "True")
        .WithProperty("PackageVersion", packageVersion)
        .WithProperty("Version", assemblyVersion)
        .WithProperty("FileVersion", fileVersion)
        .WithProperty("InformationalVersion", infoVersion)
        .WithProperty("PackageOutputPath", MakeAbsolute((DirectoryPath)"./output/").FullPath);
    MSBuild (proj, settings);

    settings.WithProperty("PackageVersion", packageVersion + "-preview" + previewNumber);
    MSBuild (proj, settings);

    Information("Pack complete.");
});

Task("Default")
    .IsDependentOn("Externals")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
