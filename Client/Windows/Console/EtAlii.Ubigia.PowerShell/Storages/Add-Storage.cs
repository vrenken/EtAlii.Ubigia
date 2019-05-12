namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [Cmdlet(VerbsCommon.Add, Nouns.Storage, DefaultParameterSetName = "byStorage")]
    public class AddStorage : TaskCmdlet<Storage>, IStorageInfoProvider
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

        protected override async Task BeginProcessingTask()
        {
            TargetStorage = await PowerShellClient.Current.StorageResolver.Get(this, StorageCmdlet.Current);

            if (TargetStorage == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            //WriteDebug($"Using storage '{TargetStorage.Name}' at {TargetStorage.Address} [{PowerShellClient.Current.Client.AuthenticationToken}]")
        }

        protected override async Task<Storage> ProcessTask()
        {
            //WriteDebug($"Adding storage {Name}")

            return await PowerShellClient.Current.ManagementConnection.Storages.Add(Name, Address);
        }
    }
}
