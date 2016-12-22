namespace EtAlii.Servus.PowerShell.Accounts
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.PowerShell.Storages;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsData.Update, Nouns.Account, DefaultParameterSetName = "byStorage")]
    public class Update_Account : StorageTargetingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "byAccount")]
        public Account Account { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (Account == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoAccount, ErrorCategory.InvalidData, null));
            }
        }

        protected override void ProcessRecord()
        {
            var task = Task.Run(async () =>
            {
                await PowerShellClient.Current.ManagementConnection.Accounts.Change(Account.Id, Account.Name, Account.Password);
            });
            task.Wait();

            var verboseDescription = String.Format("Account '{0}' has been updated.", Account.Name);
            WriteVerbose(verboseDescription);
        }
    }
}
