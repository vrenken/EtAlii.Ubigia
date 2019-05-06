namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    public static class InfrastructureConfigurationUseDiagnostics
    {
        public static InfrastructureConfiguration Use(this InfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new DiagnosticsInfrastructureExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);
        }
    }
}