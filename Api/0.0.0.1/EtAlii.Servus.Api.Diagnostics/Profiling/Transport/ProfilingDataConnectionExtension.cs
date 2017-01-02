namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingDataConnectionExtension : IDataConnectionExtension 
    {
        public void Initialize(Container container)
        {
            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Transport.Connection));
            
            container.RegisterDecorator(typeof(IDataConnection), typeof(ProfilingDataConnection));
            //container.RegisterDecorator(typeof(IEntryDataClient), typeof(ProfilingEntryDataClient));
            //container.RegisterDecorator(typeof(IEntryDataClient), typeof(DebuggingEntryDataClient));

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