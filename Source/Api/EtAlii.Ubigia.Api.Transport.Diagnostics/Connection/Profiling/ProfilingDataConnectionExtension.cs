// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingDataConnectionExtension : IDataConnectionExtension
    {
        public void Initialize(Container container)
        {
            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Transport.Connection));

            container.RegisterDecorator(typeof(IDataConnection), typeof(ProfilingDataConnection));
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
