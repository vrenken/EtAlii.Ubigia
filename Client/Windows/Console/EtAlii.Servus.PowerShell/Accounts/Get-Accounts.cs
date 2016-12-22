namespace EtAlii.Servus.PowerShell.Accounts
{
    using EtAlii.Servus.PowerShell.Storages;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;

    [Cmdlet(VerbsCommon.Get, Nouns.Accounts, DefaultParameterSetName = "byStorage")]
    [Description("Gets the accounts for the specified storage")]
    public class Get_Accounts : StorageTargetingCmdlet
    {
        protected override void ProcessRecord()
        {
            IEnumerable<Account> accounts = null;

            WriteDebug(String.Format("Getting accounts"));

            var task = Task.Run(async () =>
            {
                accounts = await PowerShellClient.Current.ManagementConnection.Accounts.GetAll();
            });
            task.Wait();

            WriteObject(accounts);
        }
    }
}
