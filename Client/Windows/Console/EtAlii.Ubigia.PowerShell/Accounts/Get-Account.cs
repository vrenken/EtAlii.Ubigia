namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    [Cmdlet(VerbsCommon.Get, Nouns.Account, DefaultParameterSetName = "byStorage")]
    public class GetAccount : AccountTargetingCmdlet
    {
        protected override void ProcessRecord()
        {
            Account account = null;

            var task = Task.Run(async () =>
            {
                account = await PowerShellClient.Current.AccountResolver.Get(this, AccountCmdlet.Current);
            });
            task.Wait();

            WriteObject(account);
        }
    }
}
