// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsStorageConnectionExtension : IStorageConnectionExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsStorageConnectionExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);

            var scaffoldings = new IScaffolding[]
            {
                new StorageConnectionLoggingScaffolding(),
                new StorageConnectionProfilingScaffolding(),
                new StorageConnectionDebuggingScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}