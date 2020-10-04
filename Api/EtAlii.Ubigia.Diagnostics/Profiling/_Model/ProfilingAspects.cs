namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System;
    using System.Linq;

    public static partial class ProfilingAspects
    {
        public static ProfilingAspect[] All => _all.Value;
        // ReSharper disable once InconsistentNaming
        private static readonly Lazy<ProfilingAspect[]> _all;

        static ProfilingAspects()
        {
            _all = new Lazy<ProfilingAspect[]>(GetAll);
        }

        private static ProfilingAspect[] GetAll()
        {
            return Functional.All
                .Concat(Logical.All)
                .Concat(Fabric.All)
                .Concat(Transport.All)
                .ToArray();
        }
    }
}