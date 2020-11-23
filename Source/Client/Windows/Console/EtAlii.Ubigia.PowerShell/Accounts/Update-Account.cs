namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Storages;

    [Cmdlet(VerbsData.Update, Nouns.Account, DefaultParameterSetName = "byStorage")]
    public class UpdateAccount : StorageTargetingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "byAccount")]
        public Account Account { get; set; }

        protected override async Task BeginProcessingTask()
        {
            await base.BeginProcessingTask().ConfigureAwait(false);

            if (Account == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoAccount, ErrorCategory.InvalidData, null));
            }
        }

        protected override async Task ProcessTask()
        {
            await PowerShellClient.Current.ManagementConnection.Accounts.Change(Account.Id, Account.Name, Account.Password).ConfigureAwait(false);

            //var verboseDescription = $"Account '{Account.Name}' has been updated."
            //WriteVerbose(verboseDescription)
        }
    }
}
