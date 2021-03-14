namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using System.IO;
    using Nuke.Common.CI.AzurePipelines;
    using Nuke.Common.Utilities.Collections;

#pragma warning disable CA1724
    public partial class Build
#pragma warning restore CA1724
    {
        private AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

        private Target PublishArtefactsToAzure => _ => _
            .Description("Publish artefacts")
            .DependsOn(Test)
            .Unlisted()
            //.DependsOn(PublishPackages)
            .ProceedAfterFailure()
            .Executes(() =>
            {
                // Implement as:
                // https://spzsource.github.io/azure/2020/05/08/nuke-build-on-azure-dev-ops-code-coverage-publication-issue.html

                // AzurePipelines?.UploadArtifact[ArtifactDirectory]
                //
                // AzurePipelines?.PublishCodeCoverage[
                //     AzurePipelinesCodeCoverageToolType.Cobertura,
                //     CoverageReportDirectory / "coverage.xml",
                //     CoverageReportDirectory]
            });

        private Target PublishResultsToAzure => _ => _
            .Description("Publish test results to Azure")
            .Unlisted()
            .DependsOn(CreateTestReports)
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
                    TestReportsDirectory / "Cobertura.xml",
                    TestReportsDirectory);
            });

// ============= Test Targets

    }
}
