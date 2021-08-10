// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class InfrastructureProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public InfrastructureProfilingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectProfiling) // profiling is enabled
            {
                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(services => services.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

                container.RegisterDecorator(typeof(IEntryRepository), typeof(ProfilingEntryRepositoryDecorator));
                container.RegisterDecorator(typeof(IIdentifierRepository), typeof(ProfilingIdentifierRepositoryDecorator));
                container.RegisterDecorator(typeof(IStorageRepository), typeof(ProfilingStorageRepositoryDecorator));
                container.RegisterDecorator(typeof(IAccountRepository), typeof(ProfilingAccountRepositoryDecorator));
                container.RegisterDecorator(typeof(ISpaceRepository), typeof(ProfilingSpaceRepositoryDecorator));
            }
        }
    }
}
