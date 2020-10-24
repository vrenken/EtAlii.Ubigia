﻿namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Remove, Nouns.Space, DefaultParameterSetName = "bySpaceName", SupportsShouldProcess = true)]
    public class RemoveSpace : SpaceTargetingCmdlet
    {
        [Parameter(Mandatory = false, Position = 999, HelpMessage = "Force space removal.")]
        public SwitchParameter Force { get; set; }

        protected override async Task ProcessTask()
        {
            var verboseDescription = $"Space '{TargetSpace.Name}' has been removed.";
            //var verboseNegativeDescription = $"Space '{TargetSpace.Name}' has not been removed."
            var verboseWarning = $"Are you sure you want to remove space '{TargetSpace.Name}'?";
            var caption = "Remove space";
            if (ShouldProcess(verboseDescription, verboseWarning, caption))
            {
                if (Force || ShouldContinue(verboseWarning, caption))
                {
                    //WriteVerbose(verboseDescription)
                    //WriteDebug($"Removing space [{TargetSpace.Name}]")
                    await PowerShellClient.Current.ManagementConnection.Spaces.Remove(TargetSpace.Id);
                    if (SpaceCmdlet.Current != null && SpaceCmdlet.Current.Id == TargetSpace.Id)
                    {
                        SpaceCmdlet.Current = null;
                    }

                }
                else
                {
                    //WriteVerbose(verboseNegativeDescription)
                }
            }
            else
            {
                //WriteVerbose(verboseNegativeDescription)
            }
        }
    }
}