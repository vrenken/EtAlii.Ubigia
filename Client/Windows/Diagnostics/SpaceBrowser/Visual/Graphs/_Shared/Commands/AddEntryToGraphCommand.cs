
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System;

    public class AddEntryToGraphCommand : CommandBase
    {
        public IReadOnlyEntry Entry { get; }
        public ProcessReason ProcessReason { get; }
        public IGraphDocumentViewModel GraphDocumentViewModel { get; }
        public int Time { get; }

        public AddEntryToGraphCommand(IReadOnlyEntry entry, ProcessReason processReason, IGraphDocumentViewModel graphDocumentViewModel)
        {
            Entry = entry;
            ProcessReason = processReason;
            GraphDocumentViewModel = graphDocumentViewModel;
            Time = Environment.TickCount;
        }

        public override string ToString()
        {
            return String.Format("{0} - Id: {1}, Reason: {2}, Time:{3}", base.GetType(), Entry.Id.ToTimeString(), ProcessReason, Time);
        }
    }
}
