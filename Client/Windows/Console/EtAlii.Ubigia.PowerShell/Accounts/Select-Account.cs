namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Roots;
    using EtAlii.Ubigia.PowerShell.Spaces;

    [Cmdlet(VerbsCommon.Select, Nouns.Account, DefaultParameterSetName = "byStorage")]
    public class SelectAccount : AccountTargetingCmdlet<Account>
    {
        protected override async Task BeginProcessingTask()
        {
            await base.BeginProcessingTask();

            AccountCmdlet.Current = null;
            SpaceCmdlet.Current = null;
            RootCmdlet.Current = null;
        }

        protected override async Task<Account> ProcessTask()
        {
            var account = await PowerShellClient.Current.AccountResolver.Get(this, AccountCmdlet.Current);

            AccountCmdlet.Current = account;
            return account;;
        } 
    }
}
