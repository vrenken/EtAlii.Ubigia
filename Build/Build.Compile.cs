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
            .Description("Compile the Ubigia solution")
            .DependsOn(PrepareSonarQubeAnalysis)
            .Unlisted()
            .Executes(CompileInternal);

        private void CompileInternal()
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetNoRestore(true)
                .SetProperty("UbigiaIsRunningOnBuildAgent", "true")
                .SetConfiguration(Configuration)
            );
        }

// ============= Test Targets 

        Target RunCompile => _ => _
            .Executes(CompileInternal);
    }
}