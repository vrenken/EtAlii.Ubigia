namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    public static class GraphQLQueryContextProfilingExtension
    {
        public static IGraphQLQueryContext CreateForProfiling(this GraphQLQueryContextFactory factory, GraphQLQueryContextConfiguration configuration)
        {
            var extensions = new IGraphQLQueryContextExtension[]
            {
                new ProfilingGraphQLQueryContextExtension(),
            };

            configuration.Use(extensions);

            return (IProfilingGraphQLQueryContext)factory.Create(configuration);
        }
    }
}