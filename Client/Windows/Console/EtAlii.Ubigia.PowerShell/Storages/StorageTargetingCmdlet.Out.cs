namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public abstract class StorageTargetingCmdlet<TOut> : TaskCmdlet<TOut>, IStorageInfoProvider
        where TOut: class
    {
        [Parameter(Mandatory = false, Position = 100, ParameterSetName = "byStorage", HelpMessage = "The storage on which the action should be applied.")]
        public Storage Storage { get; set; }

        [Parameter(Mandatory = false, Position = 100, ParameterSetName = "byStorageName", HelpMessage = "The name of the storage on which the action should be applied.")]
        public string StorageName { get; set; }

        [Parameter(Mandatory = false, Position = 100, ParameterSetName = "byStorageId", HelpMessage = "The ID of the storage on which the action should be applied.")]
        public Guid StorageId { get; set; }

        public Storage TargetStorage { get; private set; }

        protected override async Task BeginProcessingTask()
        {
            TargetStorage = await PowerShellClient.Current.StorageResolver.Get(this, StorageCmdlet.Current);

            if (TargetStorage == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            //WriteDebug($"Using storage '{TargetStorage.Name}' at {TargetStorage.Address} [{PowerShellClient.Current.Client.AuthenticationToken}]")
        }
    }
}
