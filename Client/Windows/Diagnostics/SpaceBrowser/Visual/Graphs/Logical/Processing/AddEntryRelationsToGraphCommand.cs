
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;

    public class AddEntryRelationsToGraphCommand : CommandBase
    {
        public ProcessReason ProcessReason { get; private set; }

        public AddEntryRelationsToGraphCommand(ProcessReason processReason)
        {
            ProcessReason = processReason;
        }
        
        //public override string ToString()
        //{
        //    return String.Format("{0} - Id: {1}, Reason: {2}", base.GetType(), Vertex.Entry.Id.ToTimeString(), ProcessReason);
        //}
    }
}
