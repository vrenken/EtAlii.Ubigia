namespace EtAlii.Servus.PowerShell.Spaces
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Remove, Nouns.Space, DefaultParameterSetName = "bySpaceName", SupportsShouldProcess = true)]
    public class Remove_Space : SpaceTargetingCmdlet
    {
        [Parameter(Mandatory = false, Position = 999, HelpMessage = "Force space removal.")]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            var verboseDescription = String.Format("Space '{0}' has been removed.", TargetSpace.Name);
            var verboseNegativeDescription = String.Format("Space '{0}' has not been removed.", TargetSpace.Name);
            var verboseWarning = String.Format("Are you sure you want to remove space '{0}'?", TargetSpace.Name);
            var caption = "Remove space";
            if (ShouldProcess(verboseDescription, verboseWarning, caption))
            {
                if (Force || ShouldContinue(verboseWarning, caption))
                {
                    WriteVerbose(verboseDescription);
                    WriteDebug(String.Format("Removing space [{0}]", TargetSpace.Name));
                    var task = Task.Run(async () =>
                    {
                        await PowerShellClient.Current.ManagementConnection.Spaces.Remove(TargetSpace.Id);
                    });
                    task.Wait();
                    if (SpaceCmdlet.Current != null && SpaceCmdlet.Current.Id == TargetSpace.Id)
                    {
                        SpaceCmdlet.Current = null;
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
