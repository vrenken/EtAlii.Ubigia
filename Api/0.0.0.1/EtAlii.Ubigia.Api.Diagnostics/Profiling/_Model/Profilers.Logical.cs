namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    public static partial class ProfilingAspects
    {
        public static readonly LogicalProfilers Logical = new LogicalProfilers();
    }

    public class LogicalProfilers
    {
        public ProfilingAspect[] All { get; }

        public ProfilingAspect Context { get; } = new ProfilingAspect(ProfilingLayer.Logical, "Context");

        public ProfilingAspect NodeSet { get; } = new ProfilingAspect(ProfilingLayer.Logical, "Node set");

        public ProfilingAspect TemporalWeaver { get; } = new ProfilingAspect(ProfilingLayer.Logical, "Temporal weaver");

        public ProfilingAspect Traversal { get; } = new ProfilingAspect(ProfilingLayer.Logical, "Traversal");

        public ProfilingAspect Traversers { get; } = new ProfilingAspect(ProfilingLayer.Logical, "Traversers");

        public ProfilingAspect Content { get; } = new ProfilingAspect(ProfilingLayer.Logical, "Content");

        public ProfilingAspect Properties { get; } = new ProfilingAspect(ProfilingLayer.Logical, "Properties");

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