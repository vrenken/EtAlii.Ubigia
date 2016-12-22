namespace EtAlii.Servus.PowerShell.Accounts
{
    using EtAlii.Servus.PowerShell.Storages;
    using System;
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

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

            WriteDebug(String.Format("Adding account [{0}]", AccountName));
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
