
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Collections.Generic;

    public class RemoveEntriesFromGraphCommand : CommandBase<RemoveEntriesFromGraphCommandHandler>
    {
        public IEnumerable<Identifier> Identifiers { get; private set; }
        public ProcessReason ProcessReason { get; private set; }
        public int Time { get; set; }

        public RemoveEntriesFromGraphCommand(Identifier identifier, ProcessReason processReason)
            : this(new Identifier[] { identifier }, processReason)
        {
        }

        public RemoveEntriesFromGraphCommand(IEnumerable<Identifier> identifiers, ProcessReason processReason)
        {
            Identifiers = identifiers;
            ProcessReason = processReason;
            Time = Environment.TickCount;
        }

        public override string ToString()
        {
            return String.Format("{0}, Reason: {1}, Time:{2}", base.GetType(), ProcessReason, Time);
        }
    }
}
