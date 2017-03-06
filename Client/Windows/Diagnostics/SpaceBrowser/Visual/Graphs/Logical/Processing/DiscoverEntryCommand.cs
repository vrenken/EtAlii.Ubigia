namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System;

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
            return String.Format("{0} - Id: {1}, Reason: {2}, Depth: {3}", GetType(), Entry.Id.ToTimeString(), ProcessReason, Depth);
        }
    }
}
