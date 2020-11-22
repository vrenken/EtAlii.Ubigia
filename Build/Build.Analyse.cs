namespace EtAlii.Ubigia.Pipelines
{
    using System.Collections.Generic;
    using System.Linq;
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Tools.SonarScanner;
    using static Nuke.Common.Tools.SonarScanner.SonarScannerTasks;

    public partial class Build
    {
        const string SonarQubeProjectKey = "EtAlii.Ubigia";

        const string SonarQubeServerUrl = "http://vrenken.duckdns.org:54001";
        const string SonarQubeToken = "fa1be2f386ba177214406e68fb26533f6a8981be";

        IEnumerable<string> OpenCoverTestReports => TestResultsDirectory.GlobFiles("*.xml").Select(p => p.ToString());

        Target PrepareAnalysis => _ => _
            .Description("Prepare SonarQube analysis")
            .DependsOn(Restore)
            //.Requires(() => NuGetFeedApiKey != null)
            .Executes(() =>
            {
                SonarScannerBegin(c => c
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
                SonarScannerEnd(c => c
                    .SetFramework("net5.0")
                    .SetLogin(SonarQubeToken));
            });
    }
}