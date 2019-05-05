namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    public static class IInfrastructureConfigurationDiagnosticsExtension
    {
        public static IInfrastructureConfiguration Use(this IInfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new DiagnosticsInfrastructureExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);
        }
    }
}