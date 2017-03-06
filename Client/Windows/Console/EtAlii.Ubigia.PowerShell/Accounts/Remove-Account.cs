namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [Cmdlet(VerbsCommon.Remove, Nouns.Account, DefaultParameterSetName = "byStorage", SupportsShouldProcess = true)]
    public class Remove_Account : AccountTargetingCmdlet, IAccountInfoProvider
    {
        [Parameter(Mandatory = false, Position = 999, HelpMessage = "Force account removal.")]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            var verboseDescription = String.Format("Account '{0}' has been removed.", TargetAccount.Name);
            var verboseNegativeDescription = String.Format("Account '{0}' has not been removed.", TargetAccount.Name);
            var verboseWarning = String.Format("Are you sure you want to remove account '{0}'?", TargetAccount.Name);
            var caption = "Remove account";
            if (ShouldProcess(verboseDescription, verboseWarning, caption))
            {
                if (Force || ShouldContinue(verboseWarning, caption))
                {
                    WriteVerbose(verboseDescription);
                    WriteDebug(String.Format("Removing account [{0}]", TargetAccount.Name));
                    var task = Task.Run(async () =>
                    {
                        await PowerShellClient.Current.ManagementConnection.Accounts.Remove(TargetAccount.Id);
                    });
                    task.Wait();
                    if (AccountCmdlet.Current != null && AccountCmdlet.Current.Id == TargetAccount.Id)
                    {
                        AccountCmdlet.Current = null;
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
