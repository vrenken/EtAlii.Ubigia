namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;

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
    }
}