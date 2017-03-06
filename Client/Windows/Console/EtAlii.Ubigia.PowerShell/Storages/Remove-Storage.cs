namespace EtAlii.Ubigia.PowerShell.Storages
{
    using EtAlii.Ubigia.Api;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [Cmdlet(VerbsCommon.Remove, Nouns.Storage, DefaultParameterSetName = "byStorage", SupportsShouldProcess = true)]
    public class Remove_Storage : StorageCmdlet, IStorageInfoProvider
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorage", HelpMessage = "The storage that should be removed.")]
        public Storage Storage { get; set; }

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorageName", HelpMessage = "The name of the storage that should be removed.")]
        public string StorageName { get; set; }

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "byStorageId", HelpMessage = "The ID of the storage that should be removed.")]
        public Guid StorageId { get; set; }

        [Parameter(Mandatory = false, Position = 999, HelpMessage = "Force storage removal.")]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            Storage storage = null;

            var task = Task.Run(async () =>
            {
                storage = await PowerShellClient.Current.StorageResolver.Get(this, StorageCmdlet.Current, false);
            });
            task.Wait();

            var verboseDescription = String.Format("Storage '{0}' has been removed.", storage.Name);
            var verboseNegativeDescription = String.Format("Storage '{0}' has not been removed.", storage.Name);
            var verboseWarning = String.Format("Are you sure you want to remove storage '{0}'?", storage.Name);
            var caption = "Remove storage";
            if (ShouldProcess(verboseDescription, verboseWarning, caption))
            {
                if (Force || ShouldContinue(verboseWarning, caption))
                {
                    WriteVerbose(verboseDescription);
                    WriteDebug(String.Format("Removing storage [{0}]", storage.Name));
                    task = Task.Run(async () =>
                    {
                        await PowerShellClient.Current.ManagementConnection.Storages.Remove(storage.Id);
                    });
                    task.Wait();
                }
                else
                {
                    WriteVerbose(verboseNegativeDescription);
                }
            }
            else
            {
                WriteVerbose(verboseNegativeDescription);
            }
        }
    }
}
