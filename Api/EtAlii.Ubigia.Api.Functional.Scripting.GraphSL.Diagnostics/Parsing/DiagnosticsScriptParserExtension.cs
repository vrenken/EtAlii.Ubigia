namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.xTechnology.Diagnostics;
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
                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"),
                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"));

            container.Register(() => diagnostics);

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