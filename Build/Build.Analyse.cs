namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Tools.SonarScanner;

    public partial class Build
    {
        const string SonarQubeProjectKey = "EtAlii.Ubigia";

        const string SonarQubeServerUrl = "http://vrenken.duckdns.org:54001";
        const string SonarQubeToken = "fa1be2f386ba177214406e68fb26533f6a8981be";

        AbsolutePath OpenCoverTestReports => TestResultsDirectory / "*.xml";

        Target PrepareAnalysis => _ => _
            .Description("Prepare SonarQube analysis")
            .DependsOn(Restore)
            //.Requires(() => NuGetFeedApiKey != null)
            .Executes(() =>
            {
                SonarScannerTasks.SonarScannerBegin(c => c
                    .SetFramework("net5.0")
                    .SetSourceExclusions("**/*.Tests.cs")
                    .SetOpenCoverPaths(OpenCoverTestReports)
                    .SetProjectKey(SonarQubeProjectKey)
                    .SetServer(SonarQubeServerUrl)
                    .SetName("EtAlii.Ubigia")
                    .SetLogin(SonarQubeToken));
            });

        Target CompleteAnalysis => _ => _
            .Description("Complete SonarQube analysis")
            .DependsOn(Test)
            .ProceedAfterFailure()
            .Executes(() =>
            {
                SonarScannerTasks.SonarScannerEnd(c => c
                    .SetFramework("net5.0")
                    .SetLogin(SonarQubeToken));
            });
    }
}