namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.PowerShell.Storages;

    [Cmdlet(VerbsCommon.Add, Nouns.Account, DefaultParameterSetName = "byStorage")]
    public class AddAccount : StorageTargetingCmdlet<Account>
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the account that should be added.")]
        public string AccountName { get; set; }

        [Parameter(Mandatory = true, Position = 1, HelpMessage = "The password of the account that should be added.")]
        public string Password { get; set; }

        [Parameter(Mandatory = true, Position = 2, HelpMessage = "The template that should be added.")]
        public string Template { get; set; }

        protected override async Task<Account> ProcessTask()
        {
            //WriteDebug($"Adding account [{AccountName}]")
            var template = AccountTemplate.All.Single(t => t.Name == Template);
            var account = await PowerShellClient.Current.ManagementConnection.Accounts.Add(AccountName, Password, template);
            return account;
        }
    }
}
