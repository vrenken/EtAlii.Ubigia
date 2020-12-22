namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    //using EtAlii.Ubigia.Api.Functional.Diagnostics.Querying

    public class ProfilingGraphXLQueryContextExtension : IGraphXLQueryContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(IGraphXLContext), typeof(ProfilingGraphXLContext));

            //container.RegisterDecorator(typeof(IQueryProcessorFactory), typeof(ProfilingQueryProcessorFactory))
            //container.RegisterDecorator(typeof(IQueryParserFactory), typeof(ProfilingQueryParserFactory))

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}
