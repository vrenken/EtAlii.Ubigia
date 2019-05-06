namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphQLQueryContextConfigurationDiagnosticsExtension 
    {
        public static TGraphQLQueryContextConfiguration UseFunctionalGraphQLDiagnostics<TGraphQLQueryContextConfiguration>(this TGraphQLQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where TGraphQLQueryContextConfiguration : GraphQLQueryContextConfiguration
        {
            var extensions = new IGraphQLQueryContextExtension[]
            {
                new DiagnosticsGraphQLQueryContextExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}