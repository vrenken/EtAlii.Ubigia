namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingLinqQueryContextExtension : ILinqQueryContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(ILinqQueryContext), typeof(ProfilingLinqQueryContext));
        
            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}