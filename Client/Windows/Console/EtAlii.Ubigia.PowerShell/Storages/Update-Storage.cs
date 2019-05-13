namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    [Cmdlet(VerbsData.Update, Nouns.Storage, DefaultParameterSetName = "byStorage")]
    public class UpdateStorage : TaskCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "byStorage")]
        public Storage Storage { get; set; }

        protected override Task BeginProcessingTask()
        {
            if (StorageCmdlet.Current == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            //WriteDebug($"Using storage '{StorageCmdlet.Current.Name}' at {StorageCmdlet.Current.Address} [{PowerShellClient.Current.Client.AuthenticationToken}]")

            if (Storage == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            return Task.CompletedTask;
        }

        protected override async Task ProcessTask()
        {
            await PowerShellClient.Current.ManagementConnection.Storages.Change(Storage.Id, Storage.Name, Storage.Address);

            //var verboseDescription = $"Storage '{Storage.Name}' has been updated."
            //WriteVerbose(verboseDescription)
        } 
    }
}
