namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Structure.Workflow;

    public class RetrieveEntryCommand : CommandBase
    {
        public Identifier Identifier { get; }
        public ProcessReason ProcessReason { get; }

        public RetrieveEntryCommand(in Identifier identifier, ProcessReason processReason)
        {
            Identifier = identifier;
            ProcessReason = processReason;
        }

        public override string ToString()
        {
            return $"{base.ToString()} - Id: {Identifier.ToTimeString()}, Reason: {ProcessReason}";
        }

    }
}
