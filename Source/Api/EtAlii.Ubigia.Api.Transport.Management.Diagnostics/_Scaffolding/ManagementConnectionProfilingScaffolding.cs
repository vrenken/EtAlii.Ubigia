﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics;

using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;

internal class ManagementConnectionProfilingScaffolding : IScaffolding
{
    private readonly DiagnosticsOptions _options;

    public ManagementConnectionProfilingScaffolding(DiagnosticsOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        if (_options.InjectProfiling) // profiling is enabled
        {
            container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
            container.Register(services => services.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

            container.RegisterDecorator<IManagementConnection, ProfilingManagementConnection>();
        }
    }
}
