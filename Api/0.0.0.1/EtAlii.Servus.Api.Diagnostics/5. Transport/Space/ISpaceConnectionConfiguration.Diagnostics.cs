namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ISpaceConnectionConfigurationDiagnosticsExtension
    {
        public static ISpaceConnectionConfiguration Use(this ISpaceConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ISpaceConnectionExtension[]
            {
                new DiagnosticsSpaceConnectionExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}