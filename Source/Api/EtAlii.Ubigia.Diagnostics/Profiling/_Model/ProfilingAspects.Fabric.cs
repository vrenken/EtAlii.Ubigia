// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    public partial class ProfilingAspects
    {
        public static FabricProfilers Fabric { get; }
    }

    public class FabricProfilers
    {
        public ProfilingAspect[] All { get; }

        public ProfilingAspect Context { get; } = new(ProfilingLayer.Fabric, "Context");

        public ProfilingAspect EntryCache { get; } = new(ProfilingLayer.Fabric, "Entry cache");

        public ProfilingAspect ContentCache { get; } = new(ProfilingLayer.Fabric, "Content cache");

        public ProfilingAspect PropertyCache { get; } = new(ProfilingLayer.Fabric, "Property cache");

        public FabricProfilers()
        {
            All = new[]
            {
                Context,
                EntryCache,
                ContentCache,
                PropertyCache,
            };
        }
    }
}
