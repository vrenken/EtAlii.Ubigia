
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ProcessEntryCommand : CommandBase<ProcessEntryCommandHandler>
    {
        public IReadOnlyEntry Entry { get; private set; }
        public ProcessReason ProcessReason { get; private set; }

        public ProcessEntryCommand(IReadOnlyEntry entry, ProcessReason processReason)
        {
            Entry = entry;
            ProcessReason = processReason;
        }

        public override string ToString()
        {
            return String.Format("{0} - Id: {1}, Reason: {2}", base.GetType(), Entry.Id.ToTimeString(), ProcessReason);
        }
    }
}
