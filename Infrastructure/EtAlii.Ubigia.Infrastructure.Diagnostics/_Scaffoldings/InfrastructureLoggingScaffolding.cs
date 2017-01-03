namespace EtAlii.Ubigia.Infrastructure
{
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class InfrastructureLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public InfrastructureLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<ILogFactory>(() => _diagnostics.CreateLogFactory());
            container.Register<ILogger>(() => _diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));
            if (_diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IInfrastructure), typeof(LoggingInfrastructureDecorator));

                // Logical.
                container.RegisterDecorator(typeof(IEntryPreparer), typeof(LoggingEntryPreparerDecorator));

                // Fabric.
                container.RegisterDecorator(typeof(IEntryGetter), typeof(LoggingEntryGetterDecorator));
                container.RegisterDecorator(typeof(IEntryStorer), typeof(LoggingEntryStorerDecorator));
                container.RegisterDecorator(typeof(IEntryUpdater), typeof(LoggingEntryUpdaterDecorator));

                container.RegisterDecorator(typeof(IStorageRepository), typeof(LoggingStorageRepositoryDecorator));
            }
        }
    }
}