// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class InfrastructureProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public InfrastructureProfilingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectProfiling) // profiling is enabled
            {
                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(() => container.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

                container.RegisterDecorator(typeof(IEntryRepository), typeof(ProfilingEntryRepositoryDecorator));
                container.RegisterDecorator(typeof(IIdentifierRepository), typeof(ProfilingIdentifierRepositoryDecorator));
                container.RegisterDecorator(typeof(IStorageRepository), typeof(ProfilingStorageRepositoryDecorator));
                container.RegisterDecorator(typeof(IAccountRepository), typeof(ProfilingAccountRepositoryDecorator));
                container.RegisterDecorator(typeof(ISpaceRepository), typeof(ProfilingSpaceRepositoryDecorator));
            }
        }
    }
}
