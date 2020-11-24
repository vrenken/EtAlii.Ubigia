namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;
    using System.Collections.Generic;
    using Nuke.Common.IO;

    public partial class Build
    {
        // TODO: Implement.
        // See link below for more information:
        // https://youtu.be/yojQXa1x2nc?t=1551
        //[GitVersion] GitVersion GitVersion;

        IEnumerable<AbsolutePath> Packages => ArtifactsDirectory.GlobFiles("*.nupkg");
        
        [Parameter] string NuGetFeedApiKey;
        
        Target Restore => _ => _
            .Description("Run dotnet restore")
            .Unlisted()
            .DependsOn(Clean)
            .Executes(RestoreInternal);

        private void RestoreInternal()
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution)
                .SetConfigFile(SourceDirectory / "Nuget.config"));
        }
        
        Target CreatePackages => _ => _
            .Description("Run dotnet pack")
            .DependsOn(Test)
            .Unlisted()
            .Executes(CreatePackagesInternal);

        private void CreatePackagesInternal()
        {
            Logger.Info($"dotnet pack");

            DotNetPack(_ => _
                .SetProject(Solution.GetProject("EtAlii.Ubigia"))
                .SetOutputDirectory(ArtifactsDirectory)
                .SetVersion("0.1.0"));
        }
        
        Target PublishPackages => _ => _
            .Description("Run dotnet nuget push")
            .DependsOn(CreatePackages)
            .Unlisted()
            //.Requires(() => NuGetFeedApiKey != null)
            .Executes(PublishPackagesInternal);

        private void PublishPackagesInternal()
        {
            // DotNetNuGetPush(_ => _
            //     .SetSource("https://vrenken.pkgs.visualstudio.com/_packaging/EtAlii.Ubigia/nuget/")
            //     .SetApiKey(NuGetFeedApiKey)
            //     .CombineWith(Packages, (_, v) => _
            //         .SetTargetPath(v)));
        }

// ============= Test Targets 

        Target RunRestore => _ => _
            .Executes(RestoreInternal);
        
        Target RunCreateAndPublishPackages => _ => _
            .Executes(CreatePackagesInternal)
            .Executes(PublishPackagesInternal);
    }
}