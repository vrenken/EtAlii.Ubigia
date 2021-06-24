// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class InfrastructureProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public InfrastructureProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register(() => _diagnostics.CreateProfilerFactory());
            container.Register(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));

            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
                container.RegisterDecorator(typeof(IEntryRepository), typeof(ProfilingEntryRepositoryDecorator));
                container.RegisterDecorator(typeof(IIdentifierRepository), typeof(ProfilingIdentifierRepositoryDecorator));
                container.RegisterDecorator(typeof(IStorageRepository), typeof(ProfilingStorageRepositoryDecorator));
                container.RegisterDecorator(typeof(IAccountRepository), typeof(ProfilingAccountRepositoryDecorator));
                container.RegisterDecorator(typeof(ISpaceRepository), typeof(ProfilingSpaceRepositoryDecorator));
            }
        }
    }
}