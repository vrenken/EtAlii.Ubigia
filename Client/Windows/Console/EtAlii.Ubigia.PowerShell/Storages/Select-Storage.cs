namespace EtAlii.Ubigia.PowerShell.Storages
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Accounts;
    using EtAlii.Ubigia.PowerShell.Roots;
    using EtAlii.Ubigia.PowerShell.Spaces;
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

            Current = null;
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
	                var address = new Uri(Address, UriKind.Absolute);
                    await PowerShellClient.Current.OpenManagementConnection(address, AccountName, Password);
                });
                task.Wait();
                WriteDebug($"Using storage at {Address} [{PowerShellClient.Current.Client.AuthenticationToken}]");
                storage = PowerShellClient.Current.ManagementConnection.Storage;
            }
            catch (Exception e)
            {
                Current = null;
                ThrowTerminatingError(new ErrorRecord(e, ErrorId.AuthenticationFailed, ErrorCategory.AuthenticationError, Address));
            }

            WriteDebug($"Selecting storage [{(storage != null ? storage.Name : "NONE")}]");

            Current = storage;
            WriteObject(storage);
        }
    }
}
