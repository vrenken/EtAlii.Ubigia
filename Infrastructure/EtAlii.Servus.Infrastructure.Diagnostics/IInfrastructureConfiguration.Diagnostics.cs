namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    public static class IInfrastructureConfigurationDiagnosticsExtension
    {
        public static IInfrastructureConfiguration Use(this IInfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new DiagnosticsInfrastructureExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}