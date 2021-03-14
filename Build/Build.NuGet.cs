namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;
    using System.Collections.Generic;
    using Nuke.Common.IO;
    using Nuke.Common.Tooling;

    public partial class Build
    {
        // TODO: Implement.
        // See link below for more information:
        // https://youtu.be/yojQXa1x2nc?t=1551
        //[GitVersion] GitVersion GitVersion

        private IEnumerable<AbsolutePath> Packages => ArtifactsDirectory.GlobFiles("*.nupkg");

        [Parameter("NuGet publish feed source")] private readonly string NuGetFeedSource;
        [Parameter("NuGet publish feed token")] private readonly string NuGetFeedToken;

        private Target Restore => _ => _
            .Description("Run dotnet restore")
            .Unlisted()
            .DependsOn(Clean)
            .Executes(RestoreInternal);

        private void RestoreInternal()
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution)
                .SetConfigFile(RootDirectory / "Nuget.config"));
        }

        private Target CreatePackages => _ => _
            .Description("Run dotnet pack")
            .DependsOn(Test)
            .Unlisted()
            .Executes(CreatePackagesInternal);

        private void CreatePackagesInternal()
        {
            Logger.Info($"dotnet pack");

            DotNetPack(_ => _
                .SetProject(Solution)
                .EnableNoRestore()
                .SetOutputDirectory(ArtifactsDirectory)
                .SetVersion("0.1.0"));
        }

        private Target PublishPackages => _ => _
            .Description("Run dotnet nuget push")
            .DependsOn(CreatePackages)
            .Unlisted()
            .Requires(() => NuGetFeedToken != null)
            .Requires(() => NuGetFeedSource != null)
            .Executes(PublishPackagesInternal);

        private void PublishPackagesInternal()
        {
            DotNetNuGetPush(_ => _
                .SetSource(NuGetFeedSource)
                .SetApiKey(NuGetFeedToken)
                .EnableSkipDuplicate()
                .CombineWith(Packages, (_, v) => _
                    .SetTargetPath(v)));
        }

// ============= Test Targets

        private Target RunRestore => _ => _
            .Executes(RestoreInternal);

        private Target RunCreateAndPublishPackages => _ => _
            .Executes(CreatePackagesInternal)
            .Executes(PublishPackagesInternal);

        private Target RunCreatePackages => _ => _
            .Executes(CreatePackagesInternal);
    }
}
