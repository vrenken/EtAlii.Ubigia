namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Tools.SonarScanner;
    using static Nuke.Common.Tools.SonarScanner.SonarScannerTasks;

    public partial class Build
    {
        const string SonarQubeProjectKey = "EtAlii.Ubigia";

        //const string SonarQubeServerUrl = "http://vrenken.duckdns.org:54001";
        const string SonarQubeServerUrl = "https://sonarqube.vrenken.duckdns.org";
        const string LocalSonarQubeServerUrl = "http://192.168.1.130:9000/";
        const string SonarQubeToken = "fa1be2f386ba177214406e68fb26533f6a8981be";
        
        Target PrepareSonarQubeAnalysis => _ => _
            .Description("Prepare SonarQube analysis")
            .DependsOn(Restore)
            .Unlisted()
            .Executes(PrepareSonarQubeAnalysisInternal);

        private void PrepareSonarQubeAnalysisInternal()
        {
            SonarScannerBegin(c => c
                .SetFramework("net5.0")
                .AddCoverageExclusions(SourceDirectory / "**" / "*.Tests.cs")                             // Unit tests should not be taken into consideration with regards of testing.
                .AddTestFileExclusions(SourceDirectory / "**" / "*.Tests.cs")
                
                .AddSourceExclusions((RelativePath)"**" / FrameworkHashLibDirectory / "**" / "*.*")       // We don't want the 'old' HashLib to cloud up the SonarQube results.
                .AddCoverageExclusions((RelativePath)"**" / FrameworkHashLibDirectory / "**" / "*.*") 
                .AddTestFileExclusions((RelativePath)"**" / FrameworkHashLibDirectory / "**" / "HashesTests.cs") 
            
                .AddSourceExclusions((RelativePath)"**" / FrameworkMoppetLapaDirectory / "**" / "*.*")    // We don't want the external Moppet.Lapa library to cloud upt the SonarQube results.
                .AddCoverageExclusions((RelativePath)"**" / FrameworkMoppetLapaDirectory / "**" / "*.*") 
                
                .SetOpenCoverPaths(TestResultsDirectory / "*" / "coverage.opencover.xml")
                .SetVSTestReports(TestResultsDirectory / "*.trx")
                .SetProjectKey(SonarQubeProjectKey)
                .SetServer(IsLocalBuild ? LocalSonarQubeServerUrl : SonarQubeServerUrl)
                .SetName("EtAlii.Ubigia")
                .SetLogin(SonarQubeToken));
        }
        
        Target PublishResultsToSonarQube => _ => _
            .Description("Publish test results to SonarQube")
            .Unlisted()
            .DependsOn(CreateTestReports)
            .ProceedAfterFailure()
            .Executes(PublishResultsToSonarQubeInternal);

        private void PublishResultsToSonarQubeInternal()
        {
            SonarScannerEnd(c => c
                .SetFramework("net5.0")
                .SetLogin(SonarQubeToken));
        }

// ============= Test Targets 
        
        Target RunCompileTestAndPublishToSonarQube => _ => _
            .Executes(() =>
            {
                PrepareSonarQubeAnalysisInternal();
                CompileInternal();
                TestInternal();
                CreateTestReportsInternal();
                PublishResultsToSonarQubeInternal();
            });
        
        Target RunPublishToSonarQube => _ => _
            .Executes(() =>
            {
                PrepareSonarQubeAnalysisInternal();
                CompileInternal();
                PublishResultsToSonarQubeInternal();
            });

    }
}