namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
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
            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"),
                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"));

            container.Register(() => diagnostics);

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