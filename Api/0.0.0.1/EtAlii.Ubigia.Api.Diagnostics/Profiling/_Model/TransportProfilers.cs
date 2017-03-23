namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    public class TransportProfilers
    {
        public ProfilingAspect[] All { get; }

        public ProfilingAspect Connection { get; } = new ProfilingAspect(ProfilingLayer.Transport, "Connection");

        public ProfilingAspect EntryDataClient { get; } = new ProfilingAspect(ProfilingLayer.Transport, "Entry data client");

        public TransportProfilers()
        {
            All = new[]
            {
                Connection,
                EntryDataClient
            };
        }
    }
}