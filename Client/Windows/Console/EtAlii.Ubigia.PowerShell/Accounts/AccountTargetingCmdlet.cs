namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Storages;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class AccountTargetingCmdlet : StorageTargetingCmdlet, IAccountInfoProvider
    {
        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccount", HelpMessage = "The account on which the action should be applied.")]
        public Account Account { get; set; }

        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccountName", HelpMessage = "The name of the account on which the action should be applied.")]
        public string AccountName { get; set; }

        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccountId", HelpMessage = "The ID of the account on which the action should be applied.")]
        public Guid AccountId { get; set; }

        public Account TargetAccount { get; private set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            var task = Task.Run(async () =>
            {
                TargetAccount = await PowerShellClient.Current.AccountResolver.Get(this, AccountCmdlet.Current);
            });
            task.Wait();

            if (TargetAccount == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoAccount, ErrorCategory.InvalidData, null));
            }
            WriteDebug($"Using account [{TargetAccount.Name}]");
        }
    }
}
