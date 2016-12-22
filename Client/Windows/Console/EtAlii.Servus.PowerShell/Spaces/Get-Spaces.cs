namespace EtAlii.Servus.PowerShell.Spaces
{
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.PowerShell.Accounts;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;

    [Cmdlet(VerbsCommon.Get, Nouns.Spaces, DefaultParameterSetName = "ByAccountName")]
    [Description("Gets the spaces for the specified account")]
    public class Get_Spaces : AccountTargetingCmdlet, IAccountInfoProvider
    {
        protected override void ProcessRecord()
        {
            IEnumerable<Space> spaces = null;

            WriteDebug(String.Format("Getting spaces for [{0}]", TargetAccount.Name));

            var task = Task.Run(async () =>
            {
                spaces = await PowerShellClient.Current.ManagementConnection.Spaces.GetAll(TargetAccount.Id);
            });
            task.Wait();

            WriteObject(spaces);
        }
    }
}
