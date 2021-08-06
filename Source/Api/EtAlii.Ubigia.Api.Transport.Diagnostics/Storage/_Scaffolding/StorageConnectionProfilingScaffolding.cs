﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public StorageConnectionProfilingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectProfiling) // profiling is enabled
            {
                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(() => container.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

                //container.RegisterDecorator(typeof(IDataConnection), typeof(ProfilingDataConnection))
            }
        }
    }
}
