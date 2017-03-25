namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using EtAlii.Ubigia.PowerShell.Accounts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [Cmdlet(VerbsCommon.Get, Nouns.Spaces, DefaultParameterSetName = "ByAccountName")]
    [Description("Gets the spaces for the specified account")]
    public class Get_Spaces : AccountTargetingCmdlet, IAccountInfoProvider
    {
        protected override void ProcessRecord()
        {
            IEnumerable<Space> spaces = null;

            WriteDebug($"Getting spaces for [{TargetAccount.Name}]");

            var task = Task.Run(async () =>
            {
                spaces = await PowerShellClient.Current.ManagementConnection.Spaces.GetAll(TargetAccount.Id);
            });
            task.Wait();

            WriteObject(spaces);
        }
    }
}
