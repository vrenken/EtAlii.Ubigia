namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingLogicalContextExtension : ILogicalContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(ILogicalContext), typeof(ProfilingLogicalContext));

            container.RegisterDecorator(typeof(ILogicalRootSet), typeof(ProfilingLogicalRootSet));
            container.RegisterDecorator(typeof(IPropertiesManager), typeof(ProfilingPropertiesManager));
            container.RegisterDecorator(typeof(ILogicalNodeSet), typeof(ProfilingLogicalNodeSet));
            container.RegisterDecorator(typeof(IContentManager), typeof(ProfilingContentManager));

            container.RegisterDecorator(typeof(IGraphPathTraverserFactory), typeof(ProfilingGraphPathTraverserFactory));
            
            container.Register<IProfiler>(() => new Profiler("Context", ProfilingLayer.Logical));
        }
    }
}