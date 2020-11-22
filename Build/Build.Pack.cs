namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;

    public partial class Build
    {
        // TODO: Implement.
        // See link below for more information:
        // https://youtu.be/yojQXa1x2nc?t=1551
        //[GitVersion] GitVersion GitVersion;

        Target PackPackages => _ => _
            .Description("Run dotnet pack")
            .ProceedAfterFailure()
            .DependsOn(CompleteAnalysis)
            .Executes(() =>
            {
                Logger.Info($"dotnet pack");

                DotNetPack(_ => _
                    .SetProject(Solution.GetProject("EtAlii.Ubigia"))
                    .SetOutputDirectory(ArtifactsDirectory)
                    .SetVersion("0.1.0"));
            });
    }
}