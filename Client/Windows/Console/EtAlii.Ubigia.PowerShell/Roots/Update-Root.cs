namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Spaces;

    [Cmdlet(VerbsData.Update, Nouns.Root, DefaultParameterSetName = "byRoot")]
    public class UpdateSpace : SpaceTargetingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "byRoot")]
        public Root Root { get; set; }

        protected override async Task BeginProcessingTask()
        {
            await base.BeginProcessingTask();

            if (Root == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoRoot, ErrorCategory.InvalidData, null));
            }
        }

        protected override async Task ProcessTask()
        {
            await PowerShellClient.Current.Fabric.Roots.Change(Root.Id, Root.Name);

            //var verboseDescription = $"Root '{Root.Name}' has been updated."
            //WriteVerbose(verboseDescription)
        }
    }
}
