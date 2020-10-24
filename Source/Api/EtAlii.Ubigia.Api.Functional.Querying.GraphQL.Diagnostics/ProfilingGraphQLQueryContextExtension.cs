namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingGraphQLQueryContextExtension : IGraphQLQueryContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(IGraphQLQueryContext), typeof(ProfilingGraphQLQueryContext));

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}