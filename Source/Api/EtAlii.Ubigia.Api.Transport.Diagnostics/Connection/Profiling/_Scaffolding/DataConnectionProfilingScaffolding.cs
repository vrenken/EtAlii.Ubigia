// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.Diagnostics;
    using IProfiler = EtAlii.Ubigia.Diagnostics.Profiling.IProfiler;


    internal class DataConnectionProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public DataConnectionProfilingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectProfiling) // profiling is enabled
            {
                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(() => container.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

                container.RegisterDecorator(typeof(IDataConnection), typeof(ProfilingDataConnection));
                container.RegisterDecorator(typeof(IEntryDataClient), typeof(ProfilingEntryDataClient));

                container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Transport.Connection));

                //container.RegisterDecorator(typeof(IEntryDataClient), typeof(ProfilingEntryDataClient))
                //container.RegisterDecorator(typeof(IEntryDataClient), typeof(DebuggingEntryDataClient))

                //IEntryNotificationClient

                //IContentDataClient
                //IContentNotificationClient

                //IPropertiesDataClient
                //IPropertiesNotificationClient

                //IRootDataClient
                //IRootNotificationClient

            }
        }
    }
}
