using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// We need to give Nuke a bit more time to do it's magic.
// More info: https://github.com/nuke-build/nuke/issues/260
[CheckBuildProjectConfigurations(TimeoutInMilliseconds = 2000)] 
[ShutdownDotNetAfterServerBuild]
[AzurePipelines( 
    AzurePipelinesImage.WindowsLatest, 
    InvokedTargets = new[]
    {
        nameof(Restore),
        nameof(PrepareAnalysis),
        nameof(Compile),
        nameof(Test),
        nameof(CompleteAnalysis),
        nameof(PackPackages),
        nameof(PublishPackages),
        nameof(PublishArtefacts)
    },
    NonEntryTargets = new []
    {
        nameof(Clean), 
    },
    TriggerPathsInclude = new []
    {
        "master", 
        "feature/*", 
        "release/*"
    }
)]
public partial class Build : NukeBuild
{
    /// To run nuke locally install it as a global tool:
    /// "dotnet tool install Nuke.GlobalTool --global"
    /// 
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(build => build.Test);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;
    
    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "source";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Clean => _ => _
        .Description("Clean output")
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Description("Run dotnet restore")
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .Description("Run dotnet build")
        .DependsOn(Restore, PrepareAnalysis)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });
}
