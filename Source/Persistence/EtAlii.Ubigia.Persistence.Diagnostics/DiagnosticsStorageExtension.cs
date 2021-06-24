// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsStorageExtension : IStorageExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsStorageExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            // var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
            //     () => new DisabledLogFactory(),
            //     () => new DisabledProfilerFactory(),
            //     (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Persistence"),
            //     (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Persistence"))

            var scaffoldings = new IScaffolding[]
            {
                new DiagnosticsScaffolding(_diagnostics),
                new BlobsLoggingScaffolding(_diagnostics),
                new ComponentsProfilingScaffolding(_diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
