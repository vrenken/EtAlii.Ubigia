namespace EtAlii.Servus.PowerShell.Accounts
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.PowerShell.Storages;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    public class AccountTargetingCmdlet : StorageTargetingCmdlet, IAccountInfoProvider
    {
        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccount", HelpMessage = "The account on which the action should be applied.")]
        public Account Account { get; set; }

        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccountName", HelpMessage = "The name of the account on which the action should be applied.")]
        public string AccountName { get; set; }

        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byAccountId", HelpMessage = "The ID of the account on which the action should be applied.")]
        public Guid AccountId { get; set; }

        public Account TargetAccount { get { return _targetAccount; } private set { _targetAccount = value; } }
        private Account _targetAccount;

        public AccountTargetingCmdlet()
        {
        }

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
            WriteDebug(String.Format("Using account [{0}]", TargetAccount.Name));
        }
    }
}
