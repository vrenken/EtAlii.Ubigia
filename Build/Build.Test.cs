namespace EtAlii.Ubigia.Pipelines
{
    using System.Collections.Generic;
    using System.Linq;
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.ProjectModel;
    using Nuke.Common.Tools.Coverlet;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;
    using Nuke.Common.Tooling;

    public partial class Build
    {
        public IEnumerable<Project> TestProjects => Solution
            .GetProjects("*.Tests*")
            .Where(tp => !tp.Path.ToString().EndsWith(".shproj")) // We are not interested in .shproj files. These will mess up dotnet test.
            .Where(tp => !tp.Name.EndsWith(".WebApi.Tests")); // The WebApi tests won't run nicely on the build agent. No idea why.

        AbsolutePath TestResultsDirectory => ArtifactsDirectory / "test_results";

        const int DegreeOfParallelismOnServerTests = 8;
        const int DegreeOfParallelismOnLocalTests = 16;

        Target Test => _ => _
            .Description("Run dotnet test")
            .ProceedAfterFailure()
            .Executes(() =>
            {
                // Let's go full steam ahead when it is a local build. 
                var degreeOfParallelismWhileTesting =
                    IsLocalBuild ? DegreeOfParallelismOnLocalTests : DegreeOfParallelismOnServerTests;

                DotNetTest(_ => _
                        .EnableNoBuild()
                        .EnableNoRestore()
                        .SetResultsDirectory(TestResultsDirectory)
                        .EnableCollectCoverage()
                        .SetCoverletOutputFormat(CoverletOutputFormat.opencover)
                        .CombineWith(TestProjects, (cs, testProject) => cs
                            .SetProjectFile(testProject)
                            .SetLogger($"trx;LogFileName={testProject.Name}.trx")
                            .SetCoverletOutput(TestResultsDirectory / $"{testProject.Name}.xml")),
                    degreeOfParallelismWhileTesting, Continue);
            });
    }
}