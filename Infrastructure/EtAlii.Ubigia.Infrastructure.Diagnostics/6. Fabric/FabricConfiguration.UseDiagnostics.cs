// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Diagnostics;

    public static class FabricContextConfigurationUseDiagnostics
    {
        public static TFabricContextConfiguration Use<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where TFabricContextConfiguration : IConfiguration
        {
            var extensions = new IExtension[]
            {
                new FabricContextDiagnosticsExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);
        }
    }
}