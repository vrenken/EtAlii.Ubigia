// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingFabricContextExtension : IFabricContextExtension
    {
        public void Initialize(Container container)
        {
            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Fabric.Context));
            container.RegisterDecorator(typeof(IFabricContext), typeof(ProfilingFabricContext));
            container.RegisterDecorator(typeof(IEntryCacheHelper), typeof(ProfilingEntryCacheHelper));
            container.RegisterDecorator(typeof(IContentCacheHelper), typeof(ProfilingContentCacheHelper));
            container.RegisterDecorator(typeof(IPropertyCacheHelper), typeof(ProfilingPropertyCacheHelper));
        }
    }
}
