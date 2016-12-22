namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsScriptParserExtension : IScriptParserExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsScriptParserExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Api"),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Api"));

            container.Register<IDiagnosticsConfiguration>(() => diagnostics);

            var scaffoldings = new IScaffolding[]
            {
                new ScriptParserLoggingScaffolding(),
                new ScriptParserProfilingScaffolding(),
                new ScriptParserDebuggingScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}