namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System.Linq;

    public static partial class ProfilingAspects
    {
        public static ProfilingAspect[] All { get; }

        static ProfilingAspects()
        {
            Functional = new FunctionalProfilers();
            Logical = new LogicalProfilers();
            Fabric = new FabricProfilers();
            Transport = new TransportProfilers();
            
            All = Functional.All
                .Concat(Logical.All)
                .Concat(Fabric.All)
                .Concat(Transport.All)
                .ToArray();
        }
    }
}