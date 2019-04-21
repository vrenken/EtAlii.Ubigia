
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
        
        //public override string ToString()
        //[
        //    return String.Format("[0] - Id: [1], Reason: [2]", base.GetType(), Vertex.Entry.Id.ToTimeString(), ProcessReason)
        //]
    }
}
