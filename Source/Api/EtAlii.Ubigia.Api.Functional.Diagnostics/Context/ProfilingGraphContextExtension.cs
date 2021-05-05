namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingGraphContextExtension : IGraphContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(IGraphContext), typeof(ProfilingGraphContext));

            //container.RegisterDecorator(typeof(IQueryProcessorFactory), typeof(ProfilingQueryProcessorFactory))
            //container.RegisterDecorator(typeof(IQueryParserFactory), typeof(ProfilingQueryParserFactory))

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}
