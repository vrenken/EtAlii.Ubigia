namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Accounts;
    using EtAlii.Ubigia.PowerShell.Roots;
    using EtAlii.Ubigia.PowerShell.Spaces;

    [Cmdlet(VerbsCommon.Select, Nouns.Storage)]
    public class SelectStorage : TaskCmdlet<Storage>
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The address of the storage that should be selected.")]
        public string Address { get; set; }

        [Parameter(Mandatory = true, Position = 1, HelpMessage = "The accountname that should be used to authenticate on the storage.")]
        public string AccountName { get; set; }

        [Parameter(Mandatory = true, Position = 2, HelpMessage = "The password that should be used to authenticate on the storage.")]
        public string Password { get; set; }

        protected override Task BeginProcessingTask()
        {
            PowerShellClient.Current.Client.AuthenticationToken = null;

            StorageCmdlet.CurrentManagementApiAddress = null;
            StorageCmdlet.CurrentDataApiAddress = null;
            StorageCmdlet.CurrentStorage = null;
            AccountCmdlet.Current = null;
            SpaceCmdlet.Current = null;
            RootCmdlet.Current = null;
            return Task.CompletedTask;
        }

        protected override async Task<Storage> ProcessTask()
        {
            var storage = (Storage) null;
            Uri managementAddress = null;
            Uri dataAddress = null;
            try
            {
                managementAddress = new Uri(Address, UriKind.Absolute);
                await PowerShellClient.Current.OpenManagementConnection(managementAddress, AccountName, Password).ConfigureAwait(false);
                //WriteDebug($"Using storage at {Address} [{PowerShellClient.Current.Client.AuthenticationToken}]")
                storage = PowerShellClient.Current.ManagementConnection.Storage;
                dataAddress = PowerShellClient.Current.ManagementConnection.Details.DataAddress;
            }
            catch (Exception e)
            {
                StorageCmdlet.CurrentStorage = null;
                ThrowTerminatingError(new ErrorRecord(e, ErrorId.AuthenticationFailed, ErrorCategory.AuthenticationError, Address));
            }

            //WriteDebug($"Selecting storage [{(storage != null ? storage.Name : "NONE")}]")

            StorageCmdlet.CurrentManagementApiAddress = managementAddress;
            StorageCmdlet.CurrentDataApiAddress = dataAddress;
            StorageCmdlet.CurrentStorage = storage;
            return storage;
        }
    }
}
