namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Storages;

    public abstract class AccountTargetingCmdlet<TOut> : StorageTargetingCmdlet<TOut>, IAccountInfoProvider
        where TOut: class
    {
        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccount", HelpMessage = "The account on which the action should be applied.")]
        public Account Account { get; set; }

        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccountName", HelpMessage = "The name of the account on which the action should be applied.")]
        public string AccountName { get; set; }

        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccountId", HelpMessage = "The ID of the account on which the action should be applied.")]
        public Guid AccountId { get; set; }

        public Account TargetAccount { get; private set; }

        protected override async Task BeginProcessingTask()
        {
            await base.BeginProcessingTask();
            
            TargetAccount = await PowerShellClient.Current.AccountResolver.Get(this, AccountCmdlet.Current);

            if (TargetAccount == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoAccount, ErrorCategory.InvalidData, null));
            }
            //WriteDebug($"Using account [{TargetAccount.Name}]")
        }
    }
}
