namespace EtAlii.Ubigia.Api.Functional.Diagnostics 
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

//using EtAlii.Ubigia.Api.Functional.Diagnostics.Querying;

    public class ProfilingGraphTLQueryContextExtension : IGraphTLQueryContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(IGraphTLContext), typeof(ProfilingGraphTLContext));

            //container.RegisterDecorator(typeof(IQueryProcessorFactory), typeof(ProfilingQueryProcessorFactory));
            //container.RegisterDecorator(typeof(IQueryParserFactory), typeof(ProfilingQueryParserFactory));

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}