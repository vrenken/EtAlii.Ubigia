namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;

    public static partial class ProfilingAspects
    {
        public static readonly LogicalProfilers Logical = new LogicalProfilers();
    }

    public class LogicalProfilers
    {
        public ProfilingAspect[] All => _all;
        private readonly ProfilingAspect[] _all;

        public ProfilingAspect Context => _context;
        private readonly ProfilingAspect _context = new ProfilingAspect(ProfilingLayer.Logical, "Context");

        public ProfilingAspect NodeSet => _nodeSet;
        private readonly ProfilingAspect _nodeSet = new ProfilingAspect(ProfilingLayer.Logical, "Node set");

        public ProfilingAspect TemporalWeaver => _temporalWeaver;
        private readonly ProfilingAspect _temporalWeaver = new ProfilingAspect(ProfilingLayer.Logical, "Temporal weaver");

        public ProfilingAspect Traversal => _traversal;
        private readonly ProfilingAspect _traversal = new ProfilingAspect(ProfilingLayer.Logical, "Traversal");

        public ProfilingAspect Traversers => _traversers;
        private readonly ProfilingAspect _traversers = new ProfilingAspect(ProfilingLayer.Logical, "Traversers");

        public ProfilingAspect Content => _content;
        private readonly ProfilingAspect _content = new ProfilingAspect(ProfilingLayer.Logical, "Content");

        public ProfilingAspect Properties => _properties;
        private readonly ProfilingAspect _properties = new ProfilingAspect(ProfilingLayer.Logical, "Properties");

        public LogicalProfilers()
        {
            _all = new ProfilingAspect[]
            {
                _context,
                _nodeSet,
                _temporalWeaver,
                _traversal,
                _traversers,
                _content,
                _properties
            };
        }
    }
}