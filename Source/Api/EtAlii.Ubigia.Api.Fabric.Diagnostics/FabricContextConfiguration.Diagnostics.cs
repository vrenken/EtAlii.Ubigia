namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class FabricContextConfigurationDiagnosticsExtension
    {
        public static TFabricContextConfiguration UseFabricDiagnostics<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where TFabricContextConfiguration : IFabricContextConfiguration
        {
            var extensions = new IExtension[]
            {
                new FabricContextDiagnosticsExtension(diagnostics),
            };
            return configuration.Use(extensions);
        }
    }
}
