namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.CI;
    using Nuke.Common.CI.AzurePipelines;
    using Nuke.Common.Execution;
    using Nuke.Common.Git;
    using Nuke.Common.ProjectModel;

    // We need to give Nuke a bit more time to do it's magic.
    // More info: https://github.com/nuke-build/nuke/issues/260
    [CheckBuildProjectConfigurations(TimeoutInMilliseconds = 2000)]
    [ShutdownDotNetAfterServerBuild]
    [CustomAzurePipelines(
        AzurePipelinesImage.WindowsLatest,
        InvokedTargets = new[]
        {
            nameof(CompileTestAnalyseAndPublish)
        },
        NonEntryTargets = new []
        {
            // Unlisted targets.
            nameof(Clean),
            nameof(Restore),
            nameof(PrepareSonarQubeAnalysis),
            nameof(Compile),
            nameof(Test),
            nameof(CreatePackages),
            nameof(CreateTestReports),
            nameof(PublishResultsToSonarQube),
            nameof(PublishResultsToAzure),
            nameof(PublishPackages),
            nameof(PublishArtefactsToAzure),

            // For testing purposes.
            nameof(RunRestore),
            nameof(RunCompile),
            nameof(RunCreatePackages),
            nameof(RunCreateAndPublishPackages),
            nameof(RunCompileTestAndPublishToSonarQube),
            nameof(RunPublishToSonarQube),
            nameof(RunCreateTestReports),
            nameof(RunTestsAndCreateTestReports),
        },
        ImportSecrets = new[]
        {
            nameof(SonarQubeProjectKey),
            nameof(SonarQubeServerUrl),
            nameof(SonarQubeServerToken),
            nameof(NuGetFeedSource),
            nameof(NuGetFeedToken)
        },
        Pool = "Avalon agent pool",
        TimeoutInMinutes = 120
        // TriggerPathsInclude = Triggers are still maintained on the server.
    )]
    public partial class Build : NukeBuild
    {
        /// To run nuke locally install it as a global tool:
        /// "dotnet tool install Nuke.GlobalTool --global"
        /// Or "dotnet tool update Nuke.GlobalTool --global" to update.
        ///
        /// Support plugins are available for:
        ///   - JetBrains ReSharper        https://nuke.build/resharper
        ///   - JetBrains Rider            https://nuke.build/rider
        ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
        ///   - Microsoft VSCode           https://nuke.build/vscode

        public static int Main()
        {
            return Execute<Build>(build => build.CompileTestAnalyseAndPublish);
        }
        //public static int Main() => Execute<Build>(build => build.RunCompileAndPublishToSonarQube);


        [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
        private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

        [Solution] private readonly Solution Solution;
        [GitRepository] private readonly GitRepository GitRepository;

        [CI] private readonly AzurePipelines AzurePipelines;

        private Target CompileTestAnalyseAndPublish => _ => _
            .Description("Compile, test, analyse and publish")
            .DependsOn(PublishArtefactsToAzure)
            .DependsOn(PublishPackages)
            .DependsOn(PublishResultsToSonarQube)
            .DependsOn(PublishResultsToAzure);
    }
}
