namespace EtAlii.Ubigia.Provisioning.Diagnostics
{
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;

    public static class ProvidisioningConfigurationDiagnosticsExtension
    {
        public static IProvisioningConfiguration Use(this IProvisioningConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IProvisioningExtension[]
            {
                new DiagnosticsProvisioningExtension(diagnostics), 
            };
            return configuration
                .Use(extensions)
                .Use((IDataConnectionConfiguration c) => c.Use(diagnostics))
                .Use((IManagementConnectionConfiguration c) => c.Use(diagnostics))
                .Use((IDataContextConfiguration c) => c.Use(diagnostics));
        }
    }
}