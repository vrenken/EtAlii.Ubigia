// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    public partial class ProfilingAspects
    {
        public static LogicalProfilers Logical { get; }
    }

    public class LogicalProfilers
    {
        public ProfilingAspect[] All { get; }

        public ProfilingAspect Context { get; } = new(ProfilingLayer.Logical, "Context");

        public ProfilingAspect NodeSet { get; } = new(ProfilingLayer.Logical, "Node set");

        public ProfilingAspect TemporalWeaver { get; } = new(ProfilingLayer.Logical, "Temporal weaver");

        public ProfilingAspect Traversal { get; } = new(ProfilingLayer.Logical, "Traversal");

        public ProfilingAspect Traversers { get; } = new(ProfilingLayer.Logical, "Traversers");

        public ProfilingAspect Content { get; } = new(ProfilingLayer.Logical, "Content");

        public ProfilingAspect Properties { get; } = new(ProfilingLayer.Logical, "Properties");

        public LogicalProfilers()
        {
            All = new[]
            {
                Context,
                NodeSet,
                TemporalWeaver,
                Traversal,
                Traversers,
                Content,
                Properties
            };
        }
    }
}
