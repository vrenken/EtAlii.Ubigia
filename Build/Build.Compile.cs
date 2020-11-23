namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;

    public partial class Build
    {
        AbsolutePath SourceDirectory => RootDirectory / "Source";
        
        Target Compile => _ => _
            .Description("Run dotnet build")
            .DependsOn(PrepareAnalysis)
            .Executes(() =>
            {
                DotNetBuild(s => s
                    .SetProjectFile(Solution)
                    .SetNoRestore(true)
                    .SetProperty("UbigiaIsRunningOnBuildAgent", "true")
                    .SetConfiguration(Configuration)
                );
            });
    }
}