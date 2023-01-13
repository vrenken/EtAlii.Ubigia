// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics;

using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;

internal class FunctionalProfilingScaffolding : IScaffolding
{
    private readonly DiagnosticsOptions _options;

    public FunctionalProfilingScaffolding(DiagnosticsOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        if (_options.InjectProfiling) // profiling is enabled
        {
            container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
            container.Register(services => services.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

            //container.RegisterDecorator<IEntryRepository, ProfilingEntryRepositoryDecorator>()
            //container.RegisterDecorator<IIdentifierRepository, ProfilingIdentifierRepositoryDecorator>()
            //container.RegisterDecorator<IStorageRepository, ProfilingStorageRepositoryDecorator>()
            //container.RegisterDecorator<IAccountRepository, ProfilingAccountRepositoryDecorator>()
            //container.RegisterDecorator<ISpaceRepository, ProfilingSpaceRepositoryDecorator>()
        }
    }
}
