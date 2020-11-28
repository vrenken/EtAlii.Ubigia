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
        IEnumerable<Project> TestProjects => Solution
            .GetProjects("*.Tests*")
            .Where(tp => !tp.Path.ToString().EndsWith(".shproj")) // We are not interested in .shproj files. These will mess up dotnet test.
            .Where(tp => !tp.Name.EndsWith(".WebApi.Tests")); // The WebApi tests won't run nicely on the build agent. No idea why.

        AbsolutePath TestResultsDirectory => ArtifactsDirectory / "test_results";
        AbsolutePath TestReportsDirectory => ArtifactsDirectory / "test_reports";
        RelativePath FrameworkMoppetLapaDirectory => (RelativePath)"Frameworks" / "Moppet.Lapa";
        RelativePath FrameworkHashLibDirectory => (RelativePath)"Frameworks" / "HashLib";
        
        const int DegreeOfParallelismOnServerTests = 3;
        const int DegreeOfParallelismOnLocalTests = 16;

        Target Test => _ => _
            .Description("Run dotnet test")
            .ProceedAfterFailure()
            .Unlisted()
            .DependsOn(Compile)
            .Executes(TestInternal);
        
        private void TestInternal()
        {
                // Let's go full steam ahead when it is a local build. 
                var degreeOfParallelismWhileTesting = IsLocalBuild 
                    ? DegreeOfParallelismOnLocalTests 
                    : DegreeOfParallelismOnServerTests;

                DotNetTest(_ => _
                        .SetNoBuild(true)
                        .SetNoRestore(true)
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
        
        Target CreateTestReports => _ => _
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
                .AddFileFilters(SourceDirectory / "**" / "*.Tests.cs")                             // Unit tests should not be taken into consideration with regards of testing.
                .AddFileFilters((RelativePath)"**" / FrameworkHashLibDirectory / "**" / "*.*")     // We don't want the 'old' HashLib to cloud up the SonarQube results. 
                .AddFileFilters((RelativePath)"**" / FrameworkMoppetLapaDirectory / "**" / "*.*")  // We don't want the external Moppet.Lapa library to cloud upt the SonarQube results.

            );
        }

// ============= Test Targets 

        Target RunTestsAndCreateTestReports => _ => _
            .Executes(TestInternal)
            .Executes(CreateTestReportsInternal);
        Target RunCreateTestReports => _ => _
            .Executes(CreateTestReportsInternal);

    }
}