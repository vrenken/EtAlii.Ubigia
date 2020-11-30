namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.IO;
    using Nuke.Common.Utilities.Collections;
    using static Nuke.Common.IO.FileSystemTasks;

    public partial class Build
    {
            
        Target Clean => _ => _
            .Description("Clean output")
            .Unlisted()
            .Executes(CleanInternal);

        private void CleanInternal()
        {
            Logger.Info($"==== SonarQubeProjectKey:  {SonarQubeProjectKey ?? "NULL"}");
            Logger.Info($"==== SonarQubeServerToken: {SonarQubeServerToken ?? "NULL"}");
            Logger.Info($"==== SonarQubeServerUrl:   {SonarQubeServerUrl ?? "NULL"}");
            Logger.Info($"==== NuGetFeedToken:       {NuGetFeedToken ?? "NULL"}");
            Logger.Info($"==== NuGetFeedUrl:         {NuGetFeedUrl ?? "NULL"}");
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        }
        
// ============= Test Targets 
        
        Target RunClean => _ => _
            .Description("Clean output")
            .Executes(CleanInternal);
    }
}