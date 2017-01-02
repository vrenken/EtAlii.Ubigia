namespace EtAlii.Ubigia.Api.Management
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class ManagementConnectionLoggingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register<ILogFactory>(() => diagnostics.CreateLogFactory());
            container.Register<ILogger>(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IManagementConnection), typeof(LoggingManagementConnection));
            }
        }
    }
}
