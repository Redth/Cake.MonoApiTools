#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./",
                            title: "Cake.MonoApiTools",
                            repositoryOwner: "redth",
                            repositoryName: "Cake.MonoApiTools",
                            appVeyorAccountName: "redth",
                            shouldRunDotNetCorePack: true,
                            shouldRunInspectCode: false,
                            shouldRunDupFinder: false,
                            shouldRunCodecov: false,
                            shouldPostToSlack: false,
                            shouldRunIntegrationTests: false,
                            testFilePattern: "DO_NOT_RUN_TESTS");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] {
                                BuildParameters.RootDirectoryPath + "/Cake.MonoApiTools.Tests/*.cs" },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[FakeItEasy]*",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs;*TestProjects*");
Build.RunDotNetCore();
