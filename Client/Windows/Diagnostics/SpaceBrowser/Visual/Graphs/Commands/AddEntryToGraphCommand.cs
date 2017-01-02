
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;

    public class AddEntryToGraphCommand : CommandBase<AddEntryToGraphCommandHandler>
    {
        public IReadOnlyEntry Entry { get; private set; }
        public ProcessReason ProcessReason { get; private set; }
        public int Time { get; private set; }

        public AddEntryToGraphCommand(IReadOnlyEntry entry, ProcessReason processReason)
        {
            Entry = entry;
            ProcessReason = processReason;
            Time = Environment.TickCount;
        }

        public override string ToString()
        {
            return String.Format("{0} - Id: {1}, Reason: {2}, Time:{3}", base.GetType(), Entry.Id.ToTimeString(), ProcessReason, Time);
        }
    }
}
