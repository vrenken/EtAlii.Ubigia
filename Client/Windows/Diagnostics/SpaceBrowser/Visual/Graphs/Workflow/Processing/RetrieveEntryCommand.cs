
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;

    public class RetrieveEntryCommand : CommandBase<IRetrieveEntryCommandHandler>
    {
        public Identifier Identifier { get; private set; }
        public ProcessReason ProcessReason { get; private set; }

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
