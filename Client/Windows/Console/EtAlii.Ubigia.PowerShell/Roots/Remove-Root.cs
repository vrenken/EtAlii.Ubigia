namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Remove, Nouns.Root, DefaultParameterSetName = "byRootName", SupportsShouldProcess = true)]
    public class Remove_Root : RootTargetingCmdlet
    {
        [Parameter(Mandatory = false, Position = 999, HelpMessage = "Force root removal.")]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            var verboseDescription = String.Format("Root '{0}' has been removed.", TargetRoot.Name);
            var verboseNegativeDescription = String.Format("Root '{0}' has not been removed.", TargetRoot.Name);
            var verboseWarning = String.Format("Are you sure you want to remove root '{0}'?", TargetRoot.Name);
            var caption = "Remove root";
            if (ShouldProcess(verboseDescription, verboseWarning, caption))
            {
                if (Force || ShouldContinue(verboseWarning, caption))
                {
                    WriteVerbose(verboseDescription);
                    WriteDebug(String.Format("Removing root [{0}]", TargetRoot.Name));
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
