namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    [Cmdlet(VerbsCommon.Get, Nouns.Storages, DefaultParameterSetName = "byStorage")]
    public class GetStorages : TaskCmdlet<IEnumerable<Storage>>, IStorageInfoProvider
    {
        [Parameter(Mandatory = false, ParameterSetName = "byStorage", HelpMessage = "The storage from which the storages should be retrieved.")]
        public Storage Storage { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "byStorageName", HelpMessage = "The name of the storage from which the storages should be retrieved.")]
        public string StorageName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "byStorageId", HelpMessage = "The ID of the storage from which the storages should be retrieved.")]
        public Guid StorageId { get; set; }

        private Storage TargetStorage { get; set; }

        protected override async Task BeginProcessingTask()
        {
            TargetStorage = await PowerShellClient.Current.StorageResolver.Get(this, StorageCmdlet.CurrentStorage, StorageCmdlet.CurrentStorageApiAddress);

            if (TargetStorage == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            //WriteDebug($"Using storage '{TargetStorage.Name}' at {TargetStorage.Address} [{PowerShellClient.Current.Client.AuthenticationToken}]")
        }

        protected override async Task<IEnumerable<Storage>> ProcessTask()
        {
            //WriteDebug("Getting storages")

            return await PowerShellClient.Current.ManagementConnection.Storages.GetAll();
        }
    }
}
