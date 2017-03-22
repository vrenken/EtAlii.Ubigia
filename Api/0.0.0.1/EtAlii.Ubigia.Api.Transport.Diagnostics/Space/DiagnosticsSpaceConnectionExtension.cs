namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsSpaceConnectionExtension : ISpaceConnectionExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsSpaceConnectionExtension(IDiagnosticsConfiguration diagnostics)
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
                new SpaceConnectionLoggingScaffolding(),
                new SpaceConnectionProfilingScaffolding(),
                new SpaceConnectionDebuggingScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}