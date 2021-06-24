// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    public class TransportProfilers
    {
        public ProfilingAspect[] All { get; }

        public ProfilingAspect Connection { get; } = new(ProfilingLayer.Transport, "Connection");

        public ProfilingAspect EntryDataClient { get; } = new(ProfilingLayer.Transport, "Entry data client");

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
