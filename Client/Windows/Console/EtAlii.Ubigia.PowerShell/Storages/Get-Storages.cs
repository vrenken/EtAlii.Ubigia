namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using Storage = EtAlii.Ubigia.Api.Storage;

    [Cmdlet(VerbsCommon.Get, Nouns.Storages, DefaultParameterSetName = "byStorage")]
    public class GetStorages : StorageCmdlet, IStorageInfoProvider
    {
        [Parameter(Mandatory = false, ParameterSetName = "byStorage", HelpMessage = "The storage from which the storages should be retrieved.")]
        public Storage Storage { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "byStorageName", HelpMessage = "The name of the storage from which the storages should be retrieved.")]
        public string StorageName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "byStorageId", HelpMessage = "The ID of the storage from which the storages should be retrieved.")]
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
            WriteDebug(
                $"Using storage '{TargetStorage.Name}' at {TargetStorage.Address} [{PowerShellClient.Current.Client.AuthenticationToken}]");
        }

        protected override void ProcessRecord()
        {
            IEnumerable<Storage> storages = null;
            WriteDebug("Getting storages");

            var task = Task.Run(async () =>
            {
                storages = await PowerShellClient.Current.ManagementConnection.Storages.GetAll();
            });
            task.Wait();

            WriteObject(storages);
        }
    }
}
