namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.Diagnostics;
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
