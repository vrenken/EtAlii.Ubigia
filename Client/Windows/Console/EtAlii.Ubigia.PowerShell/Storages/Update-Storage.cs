namespace EtAlii.Ubigia.PowerShell.Storages
{
    using EtAlii.Ubigia.Api;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsData.Update, Nouns.Storage, DefaultParameterSetName = "byStorage")]
    public class Update_Storage : CmdletBase
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "byStorage")]
        public Storage Storage { get; set; }

        protected override void BeginProcessing()
        {
            if (StorageCmdlet.Current == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            WriteDebug(String.Format("Using storage '{0}' at {1} [{2}]", StorageCmdlet.Current.Name, StorageCmdlet.Current.Address, PowerShellClient.Current.Client.AuthenticationToken));

            if (Storage == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
        }

        protected override void ProcessRecord()
        {
            var task = Task.Run(async () =>
            {
                await PowerShellClient.Current.ManagementConnection.Storages.Change(Storage.Id, Storage.Name, Storage.Address);
            });
            task.Wait();

            var verboseDescription = String.Format("Storage '{0}' has been updated.", Storage.Name);
            WriteVerbose(verboseDescription);
        } 
    }
}
