using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tooling;

public partial class Build
{
    IEnumerable<Project> TestProjects => TestPartition.GetCurrent(Solution
        .GetProjects("*.Tests*")
        .Where(tp => !tp.Path.ToString().EndsWith(".shproj")) // We are not interested in .shproj files. These will mess up dotnet test.
        .Where(tp => !tp.Name.EndsWith(".WebApi.Tests"))); // The WebApi tests won't run nicely on the build agent. No idea why.

    // public IEnumerable<Project> TestProjects => Solution
    //     .GetProjects("*.Tests*")
    //     .Where(tp => !tp.Path.ToString().EndsWith(".shproj")) // We are not interested in .shproj files. These will mess up dotnet test.
    //     .Where(tp => !tp.Name.EndsWith(".WebApi.Tests")); // The WebApi tests won't run nicely on the build agent. No idea why.
 
    AbsolutePath TestResultsDirectory => ArtifactsDirectory / "test_results";

    [Partition(MaxAvailableBuildAgents)] readonly Partition TestPartition;

    const int DegreeOfParallelismOnServerTests = 8;
    const int DegreeOfParallelismOnLocalTests = 30;
    const int MaxAvailableBuildAgents = 2;
    
    Target Test => _ => _
        .DependsOn(Compile)
        .Partition(() => TestPartition)
        .Executes(() =>
        {
            // Let's go full steam ahead when it is a local build. 
            var degreeOfParallelismWhileTesting = IsLocalBuild ? DegreeOfParallelismOnLocalTests : DegreeOfParallelismOnServerTests;
            
            DotNetTest(_ => _
                .EnableNoBuild()
                .EnableNoRestore()
                .SetResultsDirectory(TestResultsDirectory)
                .EnableCollectCoverage()
                .SetCoverletOutputFormat(CoverletOutputFormat.opencover)
                .CombineWith(TestProjects, (cs, testProject) => cs
                    .SetProjectFile(testProject)
                    .SetLogger($"trx;LogFileName={testProject.Name}.trx")
                    .SetCoverletOutput(TestResultsDirectory / $"{testProject.Name}.xml")), degreeOfParallelismWhileTesting, Continue);
        });

}
