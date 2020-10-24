namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsScriptProcessorExtension : IScriptProcessorExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsScriptProcessorExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);

            var scaffoldings = new IScaffolding[]
            {
                new ScriptProcessingLoggingScaffolding(),
                new ScriptProcessingProfilingScaffolding(),
                new ScriptProcessingDebuggingScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}