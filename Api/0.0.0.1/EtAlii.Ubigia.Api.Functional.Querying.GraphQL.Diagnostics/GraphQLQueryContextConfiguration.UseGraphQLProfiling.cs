namespace EtAlii.Ubigia.Api.Functional.Querying
{
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