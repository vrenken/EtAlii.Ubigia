namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;

    public class ProcessEntryCommand : CommandBase
    {
        public IReadOnlyEntry Entry { get; }
        public ProcessReason ProcessReason { get; }

        public ProcessEntryCommand(IReadOnlyEntry entry, ProcessReason processReason)
        {
            Entry = entry;
            ProcessReason = processReason;
        }

        public override string ToString()
        {
            return $"{base.GetType()} - Id: {Entry.Id.ToTimeString()}, Reason: {ProcessReason}";
        }
    }
}
