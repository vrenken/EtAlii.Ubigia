// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.Diagnostics;
    using IProfiler = EtAlii.Ubigia.Diagnostics.Profiling.IProfiler;


    internal class DataConnectionProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public DataConnectionProfilingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectProfiling) // profiling is enabled
            {
                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(services => services.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

                container.RegisterDecorator<IDataConnection, ProfilingDataConnection>();
                container.RegisterDecorator<IEntryDataClient, ProfilingEntryDataClient>();

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
