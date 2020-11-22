namespace EtAlii.Ubigia.Pipelines
{
    using Nuke.Common;
    using Nuke.Common.Tools.DotNet;
    using static Nuke.Common.Tools.DotNet.DotNetTasks;

    public partial class Build
    {
        protected override string NuGetPackagesConfigFile => SourceDirectory / "Nuget.config";

        Target Restore => _ => _
            .Description("Run dotnet restore")
            .Executes(() =>
            {
                DotNetRestore(s => s
                    .SetProjectFile(Solution)
                    .SetConfigFile(NuGetPackagesConfigFile));
            });
    }
}