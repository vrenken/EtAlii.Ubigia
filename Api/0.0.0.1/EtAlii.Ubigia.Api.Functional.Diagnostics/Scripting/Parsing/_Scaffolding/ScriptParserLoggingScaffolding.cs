namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParserLoggingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register(() => diagnostics.CreateLogFactory());
            container.Register(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IScriptParser), typeof(LoggingScriptParser));
            }

        }
    }
}
