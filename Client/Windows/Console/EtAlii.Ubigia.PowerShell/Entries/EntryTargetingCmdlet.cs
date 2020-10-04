namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Spaces;

    public abstract class EntryTargetingCmdlet<TOut> : SpaceTargetingCmdlet<TOut>, IEntryInfoProvider
        where TOut: class
    {
        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byEntry", HelpMessage = "The entry on which the action should be applied.")]
        public Entry Entry { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byEntryId", HelpMessage = "The ID of the entry on which the action should be applied.")]
        public Identifier EntryId { get; set; }

        public Entry TargetEntry { get; private set; }

        protected override async Task BeginProcessingTask()
        {
            await base.BeginProcessingTask();

            TargetEntry = await PowerShellClient.Current.EntryResolver.Get(this, EntryCmdlet.Current);

            if (TargetEntry == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoEntry, ErrorCategory.InvalidData, null));
            }
            //WriteDebug($"Using entry [{(TargetEntry != null ? TargetEntry.Id.ToString() : "NONE")}]")
        }
    }
}
