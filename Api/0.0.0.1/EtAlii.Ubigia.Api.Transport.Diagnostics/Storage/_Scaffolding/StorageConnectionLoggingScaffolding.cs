namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionLoggingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register(() => diagnostics.CreateLogFactory());
            container.Register(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IStorageConnection), typeof(LoggingStorageConnection));
                container.RegisterDecorator(typeof(IStorageTransport), typeof(LoggingStorageTransport));
            }
        }
    }
}
