namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Collections;

    public static partial class ProfilingAspects
    {
        public static ProfilingAspect[] All { get { return _all.Value; } }
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