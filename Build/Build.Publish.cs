namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using System.IO;
    using Nuke.Common.CI.AzurePipelines;
    using Nuke.Common.Utilities.Collections;
    using Nuke.Common.Tools.SonarScanner;
    using static Nuke.Common.Tools.SonarScanner.SonarScannerTasks;

    public partial class Build
    {
        AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

        Target PublishArtefacts => _ => _
            .Description("Publish artefacts")
            .DependsOn(Test)
            //.DependsOn(PublishPackages)
            .ProceedAfterFailure()
            .Executes(() =>
            {
                // Implement as:
                // https://spzsource.github.io/azure/2020/05/08/nuke-build-on-azure-dev-ops-code-coverage-publication-issue.html

                // AzurePipelines?.UploadArtifact(ArtifactDirectory);
                //
                // AzurePipelines?.PublishCodeCoverage(
                //     AzurePipelinesCodeCoverageToolType.Cobertura,
                //     CoverageReportDirectory / "coverage.xml",
                //     CoverageReportDirectory);
            });
        
        Target PublishResultsToAzure => _ => _
            .Description("Publish test results to Azure")
            .DependsOn(CreateTestCoverageReports)
            .ProceedAfterFailure()
            .Executes(() =>
            {
                TestResultsDirectory.GlobFiles("*.trx").ForEach(x =>
                    AzurePipelines?.PublishTestResults(
                        type: AzurePipelinesTestResultsType.VSTest,
                        title: $"{Path.GetFileNameWithoutExtension(x)} ({AzurePipelines.StageDisplayName})",
                        files: new string[] { x }));   

                AzurePipelines?.PublishCodeCoverage(
                    AzurePipelinesCodeCoverageToolType.Cobertura, 
                    TestCoverageCoberturaReportFileName, 
                    TestCoverageReportsFolder);
            });
        
        Target PublishResultsToSonarQube => _ => _
            .Description("Publish test results to SonarQube")
            .DependsOn(CreateTestCoverageReports)
            .ProceedAfterFailure()
            .Executes(() =>
            {
                SonarScannerEnd(c => c
                    .SetFramework("net5.0")
                    .SetLogin(SonarQubeToken));
            });
    }
}