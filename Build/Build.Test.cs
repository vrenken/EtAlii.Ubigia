namespace EtAlii.Ubigia.Pipelines
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Nuke.Common;
    using Nuke.Common.CI.AzurePipelines;
    using Nuke.Common.IO;
    using Nuke.Common.ProjectModel;
    using Nuke.Common.Tools.Coverlet;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;
    using Nuke.Common.Tooling;
    using Nuke.Common.Utilities.Collections;

    public partial class Build
    {
        IEnumerable<Project> TestProjects => Solution
            .GetProjects("*.Tests*")
            .Where(tp => !tp.Path.ToString().EndsWith(".shproj")) // We are not interested in .shproj files. These will mess up dotnet test.
            .Where(tp => !tp.Name.EndsWith(".WebApi.Tests")); // The WebApi tests won't run nicely on the build agent. No idea why.

        AbsolutePath TestResultsDirectory => ArtifactsDirectory / "test_results";

        const int DegreeOfParallelismOnServerTests = 8;
        const int DegreeOfParallelismOnLocalTests = 16;

        Target Test => _ => _
            .Description("Run dotnet test")
            .ProceedAfterFailure()
            .DependsOn(Compile)
            .Executes(() =>
            {
                // Let's go full steam ahead when it is a local build. 
                var degreeOfParallelismWhileTesting =
                    IsLocalBuild ? DegreeOfParallelismOnLocalTests : DegreeOfParallelismOnServerTests;

                DotNetTest(_ => _
                        .SetNoBuild(true)
                        .SetNoRestore(true)
                        .SetResultsDirectory(TestResultsDirectory)
                        .EnableCollectCoverage()
                        .SetCoverletOutputFormat(CoverletOutputFormat.opencover)
                        .CombineWith(TestProjects, (cs, testProject) => cs
                            .SetProjectFile(testProject)
                            .SetLogger($"trx;LogFileName={testProject.Name}.trx")
                            .SetCoverletOutput($"{testProject.Name}.oc.xml")),
                    degreeOfParallelismWhileTesting, Continue);
            });
        
        Target PublishTestResults => _ => _
            .Description("Publish test results")
            .DependsOn(Test)
            .ProceedAfterFailure()
            .Executes(() =>
            {
                TestResultsDirectory.GlobFiles("*.trx").ForEach(x =>
                    AzurePipelines?.PublishTestResults(
                        type: AzurePipelinesTestResultsType.VSTest,
                        title: $"{Path.GetFileNameWithoutExtension(x)} ({AzurePipelines.StageDisplayName})",
                        files: new string[] { x }));                
            });
    }
}