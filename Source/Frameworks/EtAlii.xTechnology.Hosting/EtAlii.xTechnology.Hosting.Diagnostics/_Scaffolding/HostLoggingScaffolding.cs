namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Serilog;

    public class HostLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public HostLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableLogging) // logging is enabled
            {
                container.RegisterInitializer<IHost>(host =>
                {
                    var configurableHost = (IConfigurableHost)host;
                    configurableHost.ConfigureHost += webHostBuilder => webHostBuilder.UseSerilog((_, loggerConfiguration) => DiagnosticsConfiguration.Configure(loggerConfiguration), true);
                });

                // Register for logging required DI instances.
                container.RegisterDecorator(typeof(IInstanceCreator), typeof(LoggingInstanceCreator));
            }
        }
    }
}
