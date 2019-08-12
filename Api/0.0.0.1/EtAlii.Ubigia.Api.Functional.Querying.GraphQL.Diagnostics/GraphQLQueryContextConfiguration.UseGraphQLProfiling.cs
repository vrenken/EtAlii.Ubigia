namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;

    public static class GraphQLQueryContextConfigurationUseGraphQLProfiling
    {
        public static TGraphQLQueryContextConfiguration UseGraphQLProfiling<TGraphQLQueryContextConfiguration>(this TGraphQLQueryContextConfiguration configuration)
        where TGraphQLQueryContextConfiguration : GraphQLQueryContextConfiguration
        {
            var extensions = new IGraphQLQueryContextExtension[]
            {
                new ProfilingGraphQLQueryContextExtension(),
            };

            configuration.Use(extensions);

            return configuration;
        }
    }
}