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
    using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;
    using Nuke.Common.Tools.ReportGenerator;

    public partial class Build
    {
        private IEnumerable<Project> TestProjects => Solution
            .GetProjects("*.Tests*")

            // The SpaceBrowser tests won't run nicely on a headless build agent.
            .Where(tp => IsLocalBuild || !tp.Name.EndsWith(".SpaceBrowser.Tests"))

            // And all WebApi/SignalR tests.
            .Where(tp => !tp.Name.EndsWith(".WebApi.Tests"))
            .Where(tp => !tp.Name.EndsWith(".SignalR.Tests"))

            // We are also not interested in .shproj files. These will mess up dotnet test.
            .Where(tp => !tp.Path.ToString().EndsWith(".shproj"));

        private AbsolutePath TestResultsDirectory => ArtifactsDirectory / "test_results";
        private AbsolutePath TestReportsDirectory => ArtifactsDirectory / "test_reports";
        private RelativePath FrameworkMoppetLapaDirectory => (RelativePath)"Frameworks" / "Moppet.Lapa";
        private RelativePath FrameworkHashLibDirectory => (RelativePath)"Frameworks" / "HashLib";

        private const int _degreeOfParallelismOnServerTests = 5;
        private const int _degreeOfParallelismOnLocalTests = 16;

        private Target Test => _ => _
            .Description("Run dotnet test")
            .ProceedAfterFailure()
            .Unlisted()
            .DependsOn(RestoreSonarQube)
            .Executes(TestInternal);

        private void TestInternal()
        {
                // Let's go full steam ahead when it is a local build.
                var degreeOfParallelismWhileTesting = IsLocalBuild
                    ? _degreeOfParallelismOnLocalTests
                    : _degreeOfParallelismOnServerTests;

                DotNetTest(_ => _
                        .EnableNoBuild()
                        .EnableNoRestore()
                        .SetResultsDirectory(TestResultsDirectory)
                        .SetConfiguration(Configuration)
                        .EnableCollectCoverage()
                        .SetDataCollector("XPlat Code Coverage")
                        .SetCoverletOutputFormat(CoverletOutputFormat.opencover)
                        .SetProcessArgumentConfigurator(c => c.Add("-- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover"))
                        .CombineWith(TestProjects, (cs, testProject) => cs
                            .SetProjectFile(testProject)
                            .SetLogger($"trx;LogFileName={testProject.Name}.trx")),
                    degreeOfParallelismWhileTesting, Continue);
        }

        private Target CreateTestReports => _ => _
            .DependsOn(Test)
            .Unlisted()
            .Description("Create test coverage reports")
            .Executes(CreateTestReportsInternal);

        private void CreateTestReportsInternal()
        {
            ReportGenerator(c => c
                .SetFramework("net5.0")
                .SetReports(TestResultsDirectory / "*/coverage.opencover.xml")
                .SetTargetDirectory(TestReportsDirectory)
                .SetReportTypes(ReportTypes.Cobertura, ReportTypes.HtmlInline_AzurePipelines_Dark)

                // Unit tests should not be taken into consideration with regards of testing.
                .AddFileFilters(SourceDirectory / "**" / "*.Tests.cs")

                // We don't want the 'old' HashLib to cloud up the SonarQube results.
                .AddFileFilters((RelativePath)"**" / FrameworkHashLibDirectory / "**" / "*.*")

                // We don't want the external Moppet.Lapa library to cloud upt the SonarQube results.
                .AddFileFilters((RelativePath)"**" / FrameworkMoppetLapaDirectory / "**" / "*.*")

            );
        }

// ============= Test Targets

        private Target RunTestsAndCreateTestReports => _ => _
            .Executes(TestInternal)
            .Executes(CreateTestReportsInternal);
        private Target RunCreateTestReports => _ => _
            .Executes(CreateTestReportsInternal);

    }
}
