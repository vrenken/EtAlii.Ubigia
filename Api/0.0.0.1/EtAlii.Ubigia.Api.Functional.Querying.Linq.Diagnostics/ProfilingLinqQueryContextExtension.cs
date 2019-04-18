namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingLinqQueryContextExtension : ILinqQueryContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(ILinqQueryContext), typeof(ProfilingLinqQueryContext));

//            container.RegisterDecorator(typeof(INodeSet), typeof(ProfilingNodeSet))
//            container.RegisterDecorator(typeof(IChangeTracker), typeof(ProfilingChangeTracker))
//            container.RegisterDecorator(typeof(IIndexSet), typeof(ProfilingIndexSet))
        
            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}