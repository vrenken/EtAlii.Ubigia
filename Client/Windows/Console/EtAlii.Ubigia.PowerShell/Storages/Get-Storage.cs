namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    [Cmdlet(VerbsCommon.Get, Nouns.Storage, DefaultParameterSetName = "byStorage")]
    public class GetStorage : TaskCmdlet<Storage>, IStorageInfoProvider
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorage", HelpMessage = "The storage that should be retrieved.")]
        public Storage Storage { get; set; }

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorageName", HelpMessage = "The name of the storage that should be retrieved.")]
        public string StorageName { get; set; }

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorageId", HelpMessage = "The ID of the storage that should be retrieved.")]
        public Guid StorageId { get; set; }

        protected override async Task<Storage> ProcessTask()
        {
            var storage = await PowerShellClient.Current.StorageResolver.Get(this, StorageCmdlet.CurrentStorage, StorageCmdlet.CurrentStorageApiAddress, false);

            return storage;
        }
    }
}
