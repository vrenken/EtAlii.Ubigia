namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Accounts;

    [Cmdlet(VerbsCommon.Get, Nouns.Spaces, DefaultParameterSetName = "ByAccountName")]
    [Description("Gets the spaces for the specified account")]
    public class GetSpaces : AccountTargetingCmdlet<IEnumerable<Space>>
    {
        protected override async Task<IEnumerable<Space>> ProcessTask()
        {
            //WriteDebug($"Getting spaces for [{TargetAccount.Name}]")

            var spaces = await PowerShellClient.Current.ManagementConnection.Spaces
                .GetAll(TargetAccount.Id)
                .ToArrayAsync()
                .ConfigureAwait(false);

            return spaces;
        }
    }
}
