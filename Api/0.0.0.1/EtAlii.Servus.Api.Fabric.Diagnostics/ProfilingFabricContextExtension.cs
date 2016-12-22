namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Fabric;
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