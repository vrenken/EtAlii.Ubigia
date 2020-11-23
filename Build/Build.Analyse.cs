namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Tools.SonarScanner;
    using static Nuke.Common.Tools.SonarScanner.SonarScannerTasks;

    public partial class Build
    {
        const string SonarQubeProjectKey = "EtAlii.Ubigia";

        const string SonarQubeServerUrl = "http://vrenken.duckdns.org:54001";
        const string SonarQubeToken = "fa1be2f386ba177214406e68fb26533f6a8981be";

        
        // These Cobertura reports are in OpenCover format.
        AbsolutePath OpenCoverTestReports => TestResultsDirectory / "*/coverage.cobertura.xml";

        Target PrepareSonarQubeAnalysis => _ => _
            .Description("Prepare SonarQube analysis")
            .DependsOn(Restore)
            .Executes(() =>
            {
                SonarScannerBegin(c => c
                    .SetFramework("net5.0")
                    .AddSourceExclusions("**/*.Tests.cs")                       // Unit tests should not be taken into consideration with regards of testing.
                    .AddSourceExclusions("**/Frameworks/HashLib/**/*.*")        // We don't want the 'old' HashLib to cloud up the SonarQube results. 
                    .AddSourceExclusions("**/Frameworks/Moppet.Lapa/**/*.*")    // We don't want the external Moppet.Lapa library to cloud upt the SonarQube results.
                    .SetOpenCoverPaths(TestCoverageSonarQubeReport)
                    .SetProjectKey(SonarQubeProjectKey)
                    .SetServer(SonarQubeServerUrl)
                    .SetName("EtAlii.Ubigia")
                    .SetLogin(SonarQubeToken));
            });
    }
}