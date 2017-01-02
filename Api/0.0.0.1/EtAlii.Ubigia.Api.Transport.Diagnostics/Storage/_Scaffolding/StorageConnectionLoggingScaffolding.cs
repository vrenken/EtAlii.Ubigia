namespace EtAlii.Ubigia.Api.Management
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

            container.Register<ILogFactory>(() => diagnostics.CreateLogFactory());
            container.Register<ILogger>(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IStorageConnection), typeof(LoggingStorageConnection));
                container.RegisterDecorator(typeof(IStorageTransport), typeof(LoggingStorageTransport));
            }
        }
    }
}
