﻿namespace EtAlii.Ubigia.PowerShell.Storages
{
    using EtAlii.Ubigia.Api;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [Cmdlet(VerbsCommon.Remove, Nouns.Storage, DefaultParameterSetName = "byStorage", SupportsShouldProcess = true)]
    public class RemoveStorage : StorageCmdlet, IStorageInfoProvider
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
                storage = await PowerShellClient.Current.StorageResolver.Get(this, Current, false);
            });
            task.Wait();

            var verboseDescription = $"Storage '{storage.Name}' has been removed.";
            var verboseNegativeDescription = $"Storage '{storage.Name}' has not been removed.";
            var verboseWarning = $"Are you sure you want to remove storage '{storage.Name}'?";
            var caption = "Remove storage";
            if (ShouldProcess(verboseDescription, verboseWarning, caption))
            {
                if (Force || ShouldContinue(verboseWarning, caption))
                {
                    WriteVerbose(verboseDescription);
                    WriteDebug($"Removing storage [{storage.Name}]");
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
