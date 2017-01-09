
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ProcessEntryCommand : CommandBase<IProcessEntryCommandHandler>
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
