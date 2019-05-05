namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphQLQueryContextConfigurationDiagnosticsExtension 
    {
        public static GraphQLQueryContextConfiguration Use(this IGraphQLQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IGraphQLQueryContextExtension[]
            {
                new DiagnosticsGraphQLQueryContextExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            return configuration.Use(extensions);
        }
    }
}