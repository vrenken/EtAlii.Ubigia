namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    public static class GraphQLQueryContextProfilingExtension
    {
        public static IGraphQLQueryContext CreateForProfiling(this GraphQLQueryContextFactory factory, GraphQLQueryContextConfiguration configuration)
        {
            configuration.Use(new IGraphQLQueryContextExtension[]
            {
                new ProfilingGraphQLQueryContextExtension(), 
            });

            return (IProfilingGraphQLQueryContext)factory.Create(configuration);
        }
    }
}