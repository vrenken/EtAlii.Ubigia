namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;
    using Nuke.Common.Tools.ReportGenerator;

    public partial class Build
    {
        AbsolutePath TestCoverageReportsFolder => ArtifactsDirectory / "reports";

        string TestCoverageCoberturaReportFileName = "Cobertura.xml";
        AbsolutePath TestCoverageSonarQubeReport => TestCoverageReportsFolder / "SonarQube.xml";

        Target CreateTestCoverageReports => _ => _
            .DependsOn(Test)
            .Description("Create test coverage reports")
            .Executes(() =>
            {
                ReportGenerator(c => c
                    .SetFramework("net5.0")
                    .SetReports(OpenCoverTestReports)
                    .SetTargetDirectory(TestCoverageReportsFolder)
                    .SetReportTypes(ReportTypes.Cobertura, ReportTypes.SonarQube, ReportTypes.Html)
                );
            });
    }
}