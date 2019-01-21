
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;

    public class AddEntryRelationsToGraphCommand : CommandBase
    {
        public ProcessReason ProcessReason { get; }

        public AddEntryRelationsToGraphCommand(ProcessReason processReason)
        {
            ProcessReason = processReason;
        }
    }
}
