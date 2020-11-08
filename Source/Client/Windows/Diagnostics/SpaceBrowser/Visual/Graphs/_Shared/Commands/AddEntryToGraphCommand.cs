
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using EtAlii.xTechnology.Structure.Workflow;

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
            return $"{GetType()} - Id: {Entry.Id.ToTimeString()}, Reason: {ProcessReason}, Time:{Time}";
        }
    }
}
