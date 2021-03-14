namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;
    using static Nuke.Common.IO.FileSystemTasks;

    public partial class Build
    {
        private AbsolutePath SourceDirectory => RootDirectory / "Source";

        private Target CompileForSonarQube => _ => _
            .Description("Compile the Ubigia solution for sonarqube analysis")
            .DependsOn(PrepareSonarQubeAnalysis)
            .ProceedAfterFailure()
            .Unlisted()
            .Executes(CompileInternal);

        // These two targets (and the two compile steps) are used in the patch to get the
        // SonarQube analysis operational with .NET 5.0 and Roslyn code generation.
        // It should get checked somewhere in the future if this is still needed.
        private Target FreezeSonarQube => _ => _
            .Description("Freeze the sonarqube analysis")
            .DependsOn(CompileForSonarQube)
            .ProceedAfterFailure()
            .Unlisted()
            .Executes(() => RenameFile(SonarQubeAnalysisTriggerConfigurationFile, SonarQubeAnalysisTriggerConfigurationFile + ".bak", FileExistsPolicy.Overwrite));

        private Target RestoreSonarQube => _ => _
            .Description("Restore the sonarqube analysis")
            .DependsOn(Compile)
            .ProceedAfterFailure()
            .Unlisted()
            .Executes(() => RenameFile(SonarQubeAnalysisTriggerConfigurationFile + ".bak", SonarQubeAnalysisTriggerConfigurationFile, FileExistsPolicy.Overwrite));

        private Target Compile => _ => _
            .Description("Compile the Ubigia solution")
            .DependsOn(FreezeSonarQube)
            .Unlisted()
            .Executes(CompileInternal);

        private void CompileInternal()
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .EnableNoRestore()
                .SetProperty("UbigiaIsRunningOnBuildAgent", "true")
                .SetConfiguration(Configuration)
            );
        }

// ============= Test Targets

        private Target RunCompile => _ => _
            .Executes(CompileInternal);
    }
}
