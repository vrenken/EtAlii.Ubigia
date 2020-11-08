namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Structure.Workflow;

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
            return $"{GetType()} - Id: {Entry.Id.ToTimeString()}, Reason: {ProcessReason}";
        }
    }
}
