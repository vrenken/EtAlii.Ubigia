namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using EtAlii.Ubigia.PowerShell.Storages;
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    [Cmdlet(VerbsCommon.Add, Nouns.Account, DefaultParameterSetName = "byStorage")]
    public class Add_Account : StorageTargetingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the account that should be added.")]
        public string AccountName { get; set; }

        [Parameter(Mandatory = true, Position = 1, HelpMessage = "The password of the account that should be added.")]
        public string Password { get; set; }

        [Parameter(Mandatory = true, Position = 2, HelpMessage = "The template that should be added.")]
        public string Template { get; set; }

        protected override void ProcessRecord()
        {
            Account account = null;

            WriteDebug($"Adding account [{AccountName}]");
            var task = Task.Run(async () =>
            {
                var template = AccountTemplate.All.Single(t => t.Name == Template);
                account = await PowerShellClient.Current.ManagementConnection.Accounts.Add(AccountName, Password, template);
            });
            task.Wait();
            WriteObject(account);
        } 
    }
}
