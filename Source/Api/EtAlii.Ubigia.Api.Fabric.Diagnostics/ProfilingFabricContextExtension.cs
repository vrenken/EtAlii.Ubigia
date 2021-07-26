// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using IProfiler = EtAlii.Ubigia.Diagnostics.Profiling.IProfiler;

    public class ProfilingFabricContextExtension : IFabricContextExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;
        public ProfilingFabricContextExtension(IConfigurationRoot configurationRoot)
        {
            _configuration = new DiagnosticsConfigurationSection();
            configurationRoot.Bind("Api:Fabric:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            if (_configuration.InjectProfiling)
            {
                container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Fabric.Context));
                container.RegisterDecorator(typeof(IFabricContext), typeof(ProfilingFabricContext));
                container.RegisterDecorator(typeof(IEntryCacheHelper), typeof(ProfilingEntryCacheHelper));
                container.RegisterDecorator(typeof(IContentCacheHelper), typeof(ProfilingContentCacheHelper));
                container.RegisterDecorator(typeof(IPropertyCacheHelper), typeof(ProfilingPropertyCacheHelper));
            }
        }
    }
}
