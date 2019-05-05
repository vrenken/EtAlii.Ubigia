namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class SpaceConnectionConfigurationDiagnosticsExtension
    {
        public static SpaceConnectionConfiguration Use(this SpaceConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ISpaceConnectionExtension[]
            {
                new DiagnosticsSpaceConnectionExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}