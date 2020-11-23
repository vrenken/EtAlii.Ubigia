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
        //[GitVersion] GitVersion GitVersion;

        IEnumerable<AbsolutePath> Packages => ArtifactsDirectory.GlobFiles("*.nupkg");
        
        [Parameter] string NuGetFeedApiKey;
        
        Target CreatePackages => _ => _
            .Description("Run dotnet pack")
            .DependsOn(Test)
            .Executes(() =>
            {
                Logger.Info($"dotnet pack");

                DotNetPack(_ => _
                    .SetProject(Solution.GetProject("EtAlii.Ubigia"))
                    .SetOutputDirectory(ArtifactsDirectory)
                    .SetVersion("0.1.0"));
            });
        
        Target PublishPackages => _ => _
            .Description("Run dotnet nuget push")
            .DependsOn(CreatePackages)
            //.Requires(() => NuGetFeedApiKey != null)
            .Executes(() =>
            {
                // DotNetNuGetPush(_ => _
                //     .SetSource("https://vrenken.pkgs.visualstudio.com/_packaging/EtAlii.Ubigia/nuget/")
                //     .SetApiKey(NuGetFeedApiKey)
                //     .CombineWith(Packages, (_, v) => _
                //         .SetTargetPath(v)));
            });
    }
}