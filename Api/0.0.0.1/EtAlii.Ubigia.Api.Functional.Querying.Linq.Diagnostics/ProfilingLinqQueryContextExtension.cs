namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
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