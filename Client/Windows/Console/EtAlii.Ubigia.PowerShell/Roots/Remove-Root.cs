namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Remove, Nouns.Root, DefaultParameterSetName = "byRootName", SupportsShouldProcess = true)]
    public class RemoveRoot : RootTargetingCmdlet
    {
        [Parameter(Mandatory = false, Position = 999, HelpMessage = "Force root removal.")]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            var verboseDescription = $"Root '{TargetRoot.Name}' has been removed.";
            var verboseNegativeDescription = $"Root '{TargetRoot.Name}' has not been removed.";
            var verboseWarning = $"Are you sure you want to remove root '{TargetRoot.Name}'?";
            var caption = "Remove root";
            if (ShouldProcess(verboseDescription, verboseWarning, caption))
            {
                if (Force || ShouldContinue(verboseWarning, caption))
                {
                    WriteVerbose(verboseDescription);
                    WriteDebug($"Removing root [{TargetRoot.Name}]");
                    var task = Task.Run(async () =>
                    {
                        await PowerShellClient.Current.Fabric.Roots.Remove(TargetRoot.Id);
                    });
                    task.Wait();
                    if (RootCmdlet.Current != null && RootCmdlet.Current.Id == TargetRoot.Id)
                    {
                        RootCmdlet.Current = null;
                    }
                }
                else
                {
                    WriteVerbose(verboseNegativeDescription);
                }
            }
            else
            {
                WriteVerbose(verboseNegativeDescription);
            }
        }
    }
}
