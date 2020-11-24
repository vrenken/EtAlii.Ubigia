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
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        }
        
// ============= Test Targets 
        
        Target RunClean => _ => _
            .Description("Clean output")
            .Executes(CleanInternal);
    }
}