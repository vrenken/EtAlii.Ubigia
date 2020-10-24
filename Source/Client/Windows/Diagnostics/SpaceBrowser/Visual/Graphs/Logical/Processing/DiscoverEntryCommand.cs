namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;

    public class DiscoverEntryCommand : CommandBase
    {
        public IReadOnlyEntry Entry { get; }
        public ProcessReason ProcessReason { get; }
        public int Depth { get; }


        public DiscoverEntryCommand(IReadOnlyEntry entry, ProcessReason processReason, int depth)
        {
            Entry = entry;
            ProcessReason = processReason;
            Depth = depth;
        }

        public override string ToString()
        {
            return $"{GetType()} - Id: {Entry.Id.ToTimeString()}, Reason: {ProcessReason}, Depth: {Depth}";
        }
    }
}
