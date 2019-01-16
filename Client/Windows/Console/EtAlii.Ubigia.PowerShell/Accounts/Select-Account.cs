namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using EtAlii.Ubigia.PowerShell.Roots;
    using EtAlii.Ubigia.PowerShell.Spaces;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    [Cmdlet(VerbsCommon.Select, Nouns.Account, DefaultParameterSetName = "byStorage")]
    public class SelectAccount : AccountTargetingCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            AccountCmdlet.Current = null;
            SpaceCmdlet.Current = null;
            RootCmdlet.Current = null;
        }

        protected override void ProcessRecord()
        {
            Account account = null;

            var task = Task.Run(async () =>
            {
                account = await PowerShellClient.Current.AccountResolver.Get(this, AccountCmdlet.Current);
            });
            task.Wait();

            AccountCmdlet.Current = account;
            WriteObject(account);
        } 
    }
}
