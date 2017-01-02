namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingLoggingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register<ILogFactory>(() => diagnostics.CreateLogFactory());
            container.Register<ILogger>(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IScriptProcessor), typeof(LoggingScriptProcessor));
            }

        }
    }
}
