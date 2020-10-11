namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    public partial class ProfilingAspects
    {
        public static FabricProfilers Fabric { get; } 
    }

    public class FabricProfilers
    {
        public ProfilingAspect[] All { get; }

        public ProfilingAspect Context { get; } = new ProfilingAspect(ProfilingLayer.Fabric, "Context");

        public ProfilingAspect EntryCache { get; } = new ProfilingAspect(ProfilingLayer.Fabric, "Entry cache");

        public ProfilingAspect ContentCache { get; } = new ProfilingAspect(ProfilingLayer.Fabric, "Content cache");

        public ProfilingAspect PropertyCache { get; } = new ProfilingAspect(ProfilingLayer.Fabric, "Property cache");

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