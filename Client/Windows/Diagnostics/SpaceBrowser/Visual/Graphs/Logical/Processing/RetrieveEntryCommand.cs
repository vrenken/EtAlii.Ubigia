namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System;

    public class RetrieveEntryCommand : CommandBase
    {
        public Identifier Identifier { get; }
        public ProcessReason ProcessReason { get; }

        public RetrieveEntryCommand(Identifier identifier, ProcessReason processReason)
        {
            Identifier = identifier;
            ProcessReason = processReason;
        }

        public override string ToString()
        {
            return String.Format("{0} - Id: {1}, Reason: {2}", base.ToString(), Identifier.ToTimeString(), ProcessReason);
        }

    }
}
