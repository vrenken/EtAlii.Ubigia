namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Functional;

    public static class GraphQLQueryContextProfilingExtension
    {
        public static IGraphQLQueryContext CreateForProfiling(this GraphQLQueryContextFactory factory, IGraphQLQueryContextConfiguration configuration)
        {
            configuration.Use(new IGraphQLQueryContextExtension[]
            {
                new ProfilingGraphQLQueryContextExtension(), 
            });

            return (IProfilingGraphQLQueryContext)factory.Create(configuration);
        }
    }
}