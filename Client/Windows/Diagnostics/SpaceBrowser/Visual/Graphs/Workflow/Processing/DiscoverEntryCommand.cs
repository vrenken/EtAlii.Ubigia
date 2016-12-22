﻿
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;

    public class DiscoverEntryCommand : CommandBase<DiscoverEntryCommandHandler>
    {
        public IReadOnlyEntry Entry { get; private set; }
        public ProcessReason ProcessReason { get; private set; }
        public int Depth { get; private set; }


        public DiscoverEntryCommand(IReadOnlyEntry entry, ProcessReason processReason, int depth)
        {
            Entry = entry;
            ProcessReason = processReason;
            Depth = depth;
        }

        public override string ToString()
        {
            return String.Format("{0} - Id: {1}, Reason: {2}, Depth: {3}", this.GetType(), Entry.Id.ToTimeString(), ProcessReason, Depth);
        }
    }
}
