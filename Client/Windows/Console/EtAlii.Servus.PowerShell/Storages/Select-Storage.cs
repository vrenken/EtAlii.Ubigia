namespace EtAlii.Servus.PowerShell.Storages
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.PowerShell.Accounts;
    using EtAlii.Servus.PowerShell.Roots;
    using EtAlii.Servus.PowerShell.Spaces;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Select, Nouns.Storage)]
    public class Select_Storage : StorageCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The address of the storage that should be selected.")]
        public string Address { get; set; }

        [Parameter(Mandatory = true, Position = 1, HelpMessage = "The accountname that should be used to authenticate on the storage.")]
        public string AccountName { get; set; }

        [Parameter(Mandatory = true, Position = 2, HelpMessage = "The password that should be used to authenticate on the storage.")]
        public string Password { get; set; }

        protected override void BeginProcessing()
        {
            PowerShellClient.Current.Client.AuthenticationToken = null;

            StorageCmdlet.Current = null;
            AccountCmdlet.Current = null;
            SpaceCmdlet.Current = null;
            RootCmdlet.Current = null;
        }

        protected override void ProcessRecord()
        {
            var storage = (Storage) null;

            try
            {
                var task = Task.Run(async () =>
                {
                    await PowerShellClient.Current.OpenManagementConnection(Address, AccountName, Password);
                });
                task.Wait();
                WriteDebug(String.Format("Using storage at {0} [{1}]", Address, PowerShellClient.Current.Client.AuthenticationToken));
                storage = PowerShellClient.Current.ManagementConnection.Storage;
            }
            catch (Exception e)
            {
                StorageCmdlet.Current = null;
                ThrowTerminatingError(new ErrorRecord(e, ErrorId.AuthenticationFailed, ErrorCategory.AuthenticationError, Address));
            }

            WriteDebug(String.Format("Selecting storage [{0}]", storage != null ? storage.Name : "NONE"));

            StorageCmdlet.Current = storage;
            WriteObject(storage);
        }
    }
}
