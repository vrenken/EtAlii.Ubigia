namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public static class GraphQLQueryContextConfigurationDiagnosticsExtension
    {
        public static TGraphQLQueryContextConfiguration UseFunctionalGraphQLDiagnostics<TGraphQLQueryContextConfiguration>(this TGraphQLQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
            where TGraphQLQueryContextConfiguration : GraphQLQueryContextConfiguration
        {
            var extensions = new IGraphQLQueryContextExtension[]
            {
                new DiagnosticsGraphQLQueryContextExtension(diagnostics),
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseFunctionalGraphSLDiagnostics(diagnostics);
            }

            return configuration;
        }
    }
}
