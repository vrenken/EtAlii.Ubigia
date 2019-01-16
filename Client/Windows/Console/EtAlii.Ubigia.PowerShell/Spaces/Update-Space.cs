namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Accounts;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsData.Update, Nouns.Space, DefaultParameterSetName = "bySpace")]
    public class UpdateSpace : AccountTargetingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "bySpace")]
        public Space Space { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            
            if (Space == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoSpace, ErrorCategory.InvalidData, null));
            }
        }

        protected override void ProcessRecord()
        {
            var task = Task.Run(async () =>
            {
                await PowerShellClient.Current.ManagementConnection.Spaces.Change(Space.Id, Space.Name);
            });
            task.Wait();

            var verboseDescription = $"Space '{Space.Name}' has been updated.";
            WriteVerbose(verboseDescription);
        } 
    }
}
