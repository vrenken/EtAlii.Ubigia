namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Accounts;

    [Cmdlet(VerbsData.Update, Nouns.Space, DefaultParameterSetName = "bySpace")]
    public class UpdateSpace : AccountTargetingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "bySpace")]
        public Space Space { get; set; }

        protected override async Task BeginProcessingTask()
        {
            await base.BeginProcessingTask();
            
            if (Space == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoSpace, ErrorCategory.InvalidData, null));
            }
        }

        protected override async Task ProcessTask()
        {
            await PowerShellClient.Current.ManagementConnection.Spaces.Change(Space.Id, Space.Name);

            //var verboseDescription = $"Space '{Space.Name}' has been updated."
            //WriteVerbose(verboseDescription)
        } 
    }
}
