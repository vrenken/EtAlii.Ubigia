using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tooling;

public partial class Build
{
    // TODO: Implement.
    // See link below for more information:
    // https://youtu.be/yojQXa1x2nc?t=1551
    //[GitVersion] GitVersion GitVersion;

    public IEnumerable<AbsolutePath> Packages => ArtifactsDirectory.GlobFiles("*.nupkg");

    [Parameter] string NuGetFeedApiKey;
    
    Target Publish => _ => _
        //.Requires(() => NuGetFeedApiKey != null)
        .DependsOn(Pack)
        .ProceedAfterFailure()
        .Executes(() =>
        {
            DotNetNuGetPush(_ => _
                .SetSource("https://vrenken.pkgs.visualstudio.com/_packaging/EtAlii.Ubigia/nuget/")
                .SetApiKey(NuGetFeedApiKey)
                .CombineWith(Packages, (_, v) => _
                    .SetTargetPath(v)));
        });
}