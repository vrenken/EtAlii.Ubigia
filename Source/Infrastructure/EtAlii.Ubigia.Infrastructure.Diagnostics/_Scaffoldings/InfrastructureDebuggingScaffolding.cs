// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class InfrastructureDebuggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public InfrastructureDebuggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectDebugging) // debugging is enabled
            {
                container.RegisterDecorator(typeof(IEntryRepository), typeof(DebuggingEntryRepositoryDecorator));
            }
        }
    }
}
