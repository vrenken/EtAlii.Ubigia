
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Collections.Generic;

    public class RemoveEntriesFromGraphCommand : CommandBase<IRemoveEntriesFromGraphCommandHandler>
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
