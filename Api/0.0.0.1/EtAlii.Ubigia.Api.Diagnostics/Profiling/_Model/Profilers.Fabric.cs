namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    public static partial class ProfilingAspects
    {
        public static readonly FabricProfilers Fabric = new FabricProfilers();
    }

    public class FabricProfilers
    {
        public ProfilingAspect[] All => _all;
        private readonly ProfilingAspect[] _all;

        public ProfilingAspect Context => _context;
        private readonly ProfilingAspect _context = new ProfilingAspect(ProfilingLayer.Fabric, "Context");

        public ProfilingAspect EntryCache => _entryCache;
        private readonly ProfilingAspect _entryCache = new ProfilingAspect(ProfilingLayer.Fabric, "Entry cache");

        public ProfilingAspect ContentCache => _contentCache;
        private readonly ProfilingAspect _contentCache = new ProfilingAspect(ProfilingLayer.Fabric, "Content cache");

        public ProfilingAspect PropertyCache => _propertyCache;
        private readonly ProfilingAspect _propertyCache = new ProfilingAspect(ProfilingLayer.Fabric, "Property cache");

        public FabricProfilers()
        {
            _all = new ProfilingAspect[]
            {
                _context,
                _entryCache,
                _contentCache,
                _propertyCache,
            };
        }
    }
}