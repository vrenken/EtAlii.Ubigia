namespace EtAlii.Ubigia.Pipelines
{
    using System.Collections.Generic;
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;
    using Nuke.Common.Tooling;

    public partial class Build
    {
        AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

        // TODO: Implement.
        // See link below for more information:
        // https://youtu.be/yojQXa1x2nc?t=1551
        //[GitVersion] GitVersion GitVersion;

        IEnumerable<AbsolutePath> Packages => ArtifactsDirectory.GlobFiles("*.nupkg");

        [Parameter] string NuGetFeedApiKey;

        Target PublishPackages => _ => _
            .Description("Run dotnet nuget push")
            .DependsOn(PackPackages)
            //.Requires(() => NuGetFeedApiKey != null)
            .ProceedAfterFailure()
            .Executes(() =>
            {
                DotNetNuGetPush(_ => _
                    .SetSource("https://vrenken.pkgs.visualstudio.com/_packaging/EtAlii.Ubigia/nuget/")
                    .SetApiKey(NuGetFeedApiKey)
                    .CombineWith(Packages, (_, v) => _
                        .SetTargetPath(v)));
            });


        Target PublishArtefacts => _ => _
            .Description("Publish artefacts")
            .DependsOn(PublishPackages)
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