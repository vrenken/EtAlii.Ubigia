namespace EtAlii.Ubigia.PowerShell.Storages
{
    using EtAlii.Ubigia.Api;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [Cmdlet(VerbsCommon.Add, Nouns.Storage, DefaultParameterSetName = "byStorage")]
    public class Add_Storage : StorageCmdlet, IStorageInfoProvider
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the storage that should be added.")]
        public string Name { get; set; }

        [Parameter(Mandatory = true, Position = 1, HelpMessage = "The address of the storage that should be added.")]
        public string Address { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "byStorage", HelpMessage = "The storage to which the new storage should be added.")]
        public Storage Storage { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "byStorageName", HelpMessage = "The name of the storage to which the new storage should be added.")]
        public string StorageName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "byStorageId", HelpMessage = "The ID of the storage to which the new storage should be added.")]
        public Guid StorageId { get; set; }

        protected Storage TargetStorage { get; private set; }

        protected override void BeginProcessing()
        {
            var task = Task.Run(async () =>
            {
                TargetStorage = await PowerShellClient.Current.StorageResolver.Get(this, Current);
            });
            task.Wait();

            if (TargetStorage == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            WriteDebug(String.Format("Using storage '{0}' at {1} [{2}]", TargetStorage.Name, TargetStorage.Address, PowerShellClient.Current.Client.AuthenticationToken));
        }

        protected override void ProcessRecord()
        {
            Storage storage = null;

            WriteDebug(String.Format("Adding storage {0}", Name));

            var task = Task.Run(async () =>
            {
                storage = await PowerShellClient.Current.ManagementConnection.Storages.Add(Name, Address);
            });
            task.Wait();

            WriteObject(storage);
        }
    }
}
