namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphQLQueryContextConfigurationDiagnosticsExtension 
    {
        public static GraphQLQueryContextConfiguration Use(this IGraphQLQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IGraphQLQueryContextExtension[]
            {
                new DiagnosticsGraphQLQueryContextExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}