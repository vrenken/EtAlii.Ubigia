﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Structure.Workflow;

    public class RemoveEntriesFromGraphCommand : CommandBase
    {
        public IEnumerable<Identifier> Identifiers { get; }
        public ProcessReason ProcessReason { get; }
        public int Time { get; set; }

        public RemoveEntriesFromGraphCommand(in Identifier identifier, ProcessReason processReason)
            : this(new[] { identifier }, processReason)
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
            return $"{GetType()}, Reason: {ProcessReason}, Time:{Time}";
        }
    }
}
