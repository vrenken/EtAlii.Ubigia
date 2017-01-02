namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionLoggingScaffolding : IScaffolding
    {
        private readonly bool _hasExistingInfrastructureClientRegistration;

        public DataConnectionLoggingScaffolding(bool hasExistingInfrastructureClientRegistration)
        {
            _hasExistingInfrastructureClientRegistration = hasExistingInfrastructureClientRegistration;
        }

        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register<ILogFactory>(() => diagnostics.CreateLogFactory());
            container.Register<ILogger>(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                if (!_hasExistingInfrastructureClientRegistration)
                {
                    container.RegisterDecorator(typeof(IInfrastructureClient), typeof(LoggingInfrastructureClient));
                }

                container.RegisterDecorator(typeof(IStorageConnection), typeof(LoggingStorageConnection));
                container.RegisterDecorator(typeof(ITransport), typeof(LoggingTransport));
                container.RegisterDecorator(typeof(IDataConnection), typeof(LoggingDataConnection));

                container.RegisterDecorator(typeof(IRootDataClient), typeof(LoggingRootDataClient));
                container.RegisterDecorator(typeof(IEntryDataClient), typeof(LoggingEntryDataClient));
            }
        }
    }
}
