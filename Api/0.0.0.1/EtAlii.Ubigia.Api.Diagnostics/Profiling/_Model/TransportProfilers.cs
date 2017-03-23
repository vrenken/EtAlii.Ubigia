namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    public class TransportProfilers
    {
        public ProfilingAspect[] All => _all;
        private readonly ProfilingAspect[] _all;

        public ProfilingAspect Connection => _connection;
        private readonly ProfilingAspect _connection = new ProfilingAspect(ProfilingLayer.Transport, "Connection");

        public ProfilingAspect EntryDataClient => _entryDataClient;
        private readonly ProfilingAspect _entryDataClient = new ProfilingAspect(ProfilingLayer.Transport, "Entry data client");

        public TransportProfilers()
        {
            _all = new[]
            {
                _connection,
                _entryDataClient
            };
        }
    }
}