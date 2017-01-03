namespace EtAlii.Ubigia.Infrastructure
{
    using System;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class DiagnosticsScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            //var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
            //    () => new DisabledLogFactory(),
            //    () => new DisabledProfilerFactory(),
            //    (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"),
            //    (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"));

            if (_diagnostics == null)
            {
                throw new InvalidOperationException();
            }
            container.Register<IDiagnosticsConfiguration>(() => _diagnostics);
        }
    }
}