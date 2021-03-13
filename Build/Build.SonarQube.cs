namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Tools.SonarScanner;
    using static Nuke.Common.Tools.SonarScanner.SonarScannerTasks;

    public partial class Build
    {
        [Parameter("SonarQube project key")] private readonly string SonarQubeProjectKey;
        [Parameter("SonarQube server url")] private readonly string SonarQubeServerUrl;
        [Parameter("SonarQube server token")] private readonly string SonarQubeServerToken;

        private const string _localSonarQubeServerUrl = "http://192.168.1.130:9000/";

        private Target PrepareSonarQubeAnalysis => _ => _
            .Description("Prepare SonarQube analysis")
            .DependsOn(Restore)
            .Requires(() => SonarQubeProjectKey != null)
            .Requires(() => SonarQubeServerToken != null)
            .Requires(() => SonarQubeServerUrl != null)
            .Unlisted()
            .Executes(PrepareSonarQubeAnalysisInternal);

        private void PrepareSonarQubeAnalysisInternal()
        {
            SonarScannerBegin(c => c
                .SetFramework("net5.0")
                .SetWebServiceTimeout(120) // Somehow our service is currently flaky.

                // Unit tests should not be taken into consideration with regards of testing.
                .AddCoverageExclusions(SourceDirectory / "**" / "*.Tests.cs")
                .AddTestFileExclusions(SourceDirectory / "**" / "*.Tests.cs")

                // We don't want the 'old' HashLib to cloud up the SonarQube results.
                .AddSourceExclusions((RelativePath)"**" / FrameworkHashLibDirectory / "**" / "*.*")
                .AddCoverageExclusions((RelativePath)"**" / FrameworkHashLibDirectory / "**" / "*.*")
                .AddTestFileExclusions((RelativePath)"**" / FrameworkHashLibDirectory / "**" / "HashesTests.cs")

                // We don't want the external Moppet.Lapa library to cloud upt the SonarQube results.
                .AddSourceExclusions((RelativePath)"**" / FrameworkMoppetLapaDirectory / "**" / "*.*")
                .AddCoverageExclusions((RelativePath)"**" / FrameworkMoppetLapaDirectory / "**" / "*.*")

                .SetOpenCoverPaths(TestResultsDirectory / "*" / "coverage.opencover.xml")
                .SetVSTestReports(TestResultsDirectory / "*.trx")
                .SetProjectKey(SonarQubeProjectKey)
                .SetServer(IsLocalBuild ? _localSonarQubeServerUrl : SonarQubeServerUrl)
                .SetName(SonarQubeProjectKey)
                .SetLogin(SonarQubeServerToken));
        }

        private Target PublishResultsToSonarQube => _ => _
            .Description("Publish test results to SonarQube")
            .Unlisted()
            .DependsOn(CreateTestReports)
            .ProceedAfterFailure()
            .Executes(PublishResultsToSonarQubeInternal);

        private void PublishResultsToSonarQubeInternal()
        {
            SonarScannerEnd(c => c
                .SetFramework("net5.0")
                .SetLogin(SonarQubeServerToken));
        }

// ============= Test Targets

        private Target RunCompileTestAndPublishToSonarQube => _ => _
            .Executes(() =>
            {
                PrepareSonarQubeAnalysisInternal();
                CompileInternal();
                TestInternal();
                CreateTestReportsInternal();
                PublishResultsToSonarQubeInternal();
            });

        private Target RunPublishToSonarQube => _ => _
            .Executes(() =>
            {
                PrepareSonarQubeAnalysisInternal();
                CompileInternal();
                PublishResultsToSonarQubeInternal();
            });

    }
}
