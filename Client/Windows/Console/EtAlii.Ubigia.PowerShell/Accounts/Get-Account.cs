namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Get, Nouns.Account, DefaultParameterSetName = "byStorage")]
    public class GetAccount : AccountTargetingCmdlet<Account>
    {
        protected override async Task<Account> ProcessTask()
        {
            var account = await PowerShellClient.Current.AccountResolver.Get(this, AccountCmdlet.Current);
            return account;
        }
    }
}
