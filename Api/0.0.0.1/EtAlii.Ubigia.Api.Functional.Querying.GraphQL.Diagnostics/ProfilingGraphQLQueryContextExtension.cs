namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;
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