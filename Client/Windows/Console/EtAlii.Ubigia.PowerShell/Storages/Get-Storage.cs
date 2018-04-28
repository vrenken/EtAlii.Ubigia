namespace EtAlii.Ubigia.PowerShell.Storages
{
    using EtAlii.Ubigia.Api;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [Cmdlet(VerbsCommon.Get, Nouns.Storage, DefaultParameterSetName = "byStorage")]
    public class GetStorage : StorageCmdlet, IStorageInfoProvider
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorage", HelpMessage = "The storage that should be retrieved.")]
        public Storage Storage { get; set; }

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorageName", HelpMessage = "The name of the storage that should be retrieved.")]
        public string StorageName { get; set; }

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorageId", HelpMessage = "The ID of the storage that should be retrieved.")]
        public Guid StorageId { get; set; }

        protected override void ProcessRecord()
        {
            Storage storage = null;

            var task = Task.Run(async () =>
            {
                storage = await PowerShellClient.Current.StorageResolver.Get(this, Current, false);
            });
            task.Wait();

            WriteObject(storage);
        }
    }
}
