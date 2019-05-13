namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Remove, Nouns.Account, DefaultParameterSetName = "byStorage", SupportsShouldProcess = true)]
    public class RemoveAccount : AccountTargetingCmdlet
    {
        [Parameter(Mandatory = false, Position = 999, HelpMessage = "Force account removal.")]
        public SwitchParameter Force { get; set; }

        protected override async Task ProcessTask()
        {
            var verboseDescription = $"Account '{TargetAccount.Name}' has been removed.";
            //var verboseNegativeDescription = $"Account '{TargetAccount.Name}' has not been removed."
            var verboseWarning = $"Are you sure you want to remove account '{TargetAccount.Name}'?";
            var caption = "Remove account";
            if (ShouldProcess(verboseDescription, verboseWarning, caption))
            {
                if (Force || ShouldContinue(verboseWarning, caption))
                {
                    //WriteVerbose(verboseDescription)
                    //WriteDebug($"Removing account [{TargetAccount.Name}]")
                    await PowerShellClient.Current.ManagementConnection.Accounts.Remove(TargetAccount.Id);
                    if (AccountCmdlet.Current != null && AccountCmdlet.Current.Id == TargetAccount.Id)
                    {
                        AccountCmdlet.Current = null;
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
