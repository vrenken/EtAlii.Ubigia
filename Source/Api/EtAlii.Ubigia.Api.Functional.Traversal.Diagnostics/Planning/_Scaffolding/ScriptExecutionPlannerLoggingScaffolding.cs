namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptExecutionPlannerLoggingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IScriptExecutionPlanner), typeof(LoggingScriptExecutionPlanner));
            }
        }
    }
}
