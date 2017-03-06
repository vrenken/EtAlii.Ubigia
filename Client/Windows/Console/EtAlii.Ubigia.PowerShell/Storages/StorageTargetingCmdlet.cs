namespace EtAlii.Ubigia.PowerShell.Storages
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class StorageTargetingCmdlet : StorageCmdletBase, IStorageInfoProvider
    {
        [Parameter(Mandatory = false, Position = 100, ParameterSetName = "byStorage", HelpMessage = "The storage on which the action should be applied.")]
        public Storage Storage { get; set; }

        [Parameter(Mandatory = false, Position = 100, ParameterSetName = "byStorageName", HelpMessage = "The name of the storage on which the action should be applied.")]
        public string StorageName { get; set; }

        [Parameter(Mandatory = false, Position = 100, ParameterSetName = "byStorageId", HelpMessage = "The ID of the storage on which the action should be applied.")]
        public Guid StorageId { get; set; }

        public Storage TargetStorage { get { return _targetStorage; } private set { _targetStorage = value; } }
        private Storage _targetStorage;

        protected override void BeginProcessing()
        {
            var task = Task.Run(async () =>
            {
                TargetStorage = await PowerShellClient.Current.StorageResolver.Get(this, StorageCmdlet.Current);
            });
            task.Wait();

            if (TargetStorage == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            WriteDebug(String.Format("Using storage '{0}' at {1} [{2}]", TargetStorage.Name, TargetStorage.Address, PowerShellClient.Current.Client.AuthenticationToken));
        }
    }
}
