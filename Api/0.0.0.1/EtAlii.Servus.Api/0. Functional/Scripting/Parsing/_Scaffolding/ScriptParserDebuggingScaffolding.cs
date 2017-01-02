namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParserDebuggingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            if (diagnostics.EnableDebugging) // diagnostics is enabled
            {
            }
        }
    }
}
