// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using IProfiler = EtAlii.Ubigia.Diagnostics.Profiling.IProfiler;

    public sealed class ProfilingFabricContextExtension : IExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public ProfilingFabricContextExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Api:Fabric:Diagnostics")
                .Get<DiagnosticsOptions>();

            if (options?.InjectProfiling ?? false)
            {
                container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Fabric.Context));
                container.RegisterDecorator<IFabricContext, ProfilingFabricContext>();
                container.RegisterDecorator<IContentCacheHelper, ProfilingContentCacheHelper>();
                container.RegisterDecorator<IPropertyCacheHelper, ProfilingPropertyCacheHelper>();
            }
        }
    }
}
