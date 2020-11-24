namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Storages;

    [Cmdlet(VerbsCommon.Get, Nouns.Accounts, DefaultParameterSetName = "byStorage")]
    [Description("Gets the accounts for the specified storage")]
    public class GetAccounts : StorageTargetingCmdlet<IEnumerable<Account>>
    {
        protected override async Task<IEnumerable<Account>> ProcessTask()
        {
            //WriteDebug("Getting accounts")

            var accounts = await PowerShellClient.Current.ManagementConnection.Accounts
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);
            return accounts;
        }
    }
}
