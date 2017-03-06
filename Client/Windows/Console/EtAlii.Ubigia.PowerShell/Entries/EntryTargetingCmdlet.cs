namespace EtAlii.Ubigia.PowerShell
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Spaces;
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class EntryTargetingCmdlet : SpaceTargetingCmdlet, IEntryInfoProvider
    {
        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byEntry", HelpMessage = "The entry on which the action should be applied.")]
        public Entry Entry { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byEntryId", HelpMessage = "The ID of the entry on which the action should be applied.")]
        public Identifier EntryId { get; set; }

        public Entry TargetEntry { get { return _targetEntry; } private set { _targetEntry = value; } }
        private Entry _targetEntry;

        public EntryTargetingCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            var task = Task.Run(async () =>
            {
                TargetEntry = await PowerShellClient.Current.EntryResolver.Get(this, EntryCmdlet.Current);
            });
            task.Wait();

            if (TargetEntry == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoEntry, ErrorCategory.InvalidData, null));
            }
            WriteDebug(String.Format("Using entry [{0}]", TargetEntry != null ? TargetEntry.Id.ToString() : "NONE"));
        }
    }
}
