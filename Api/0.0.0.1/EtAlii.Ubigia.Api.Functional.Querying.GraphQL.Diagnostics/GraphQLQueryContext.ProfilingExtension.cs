namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System.Linq;

    public static class GraphQLQueryContextProfilingExtension
    {
        public static IGraphQLQueryContext CreateForProfiling(this GraphQLQueryContextFactory factory, GraphQLQueryContextConfiguration configuration)
        {
            var extensions = new IGraphQLQueryContextExtension[]
            {
                new ProfilingGraphQLQueryContextExtension(),
            }.Cast<IExtension>().ToArray();

            configuration.Use(extensions);

            return (IProfilingGraphQLQueryContext)factory.Create(configuration);
        }
    }
}