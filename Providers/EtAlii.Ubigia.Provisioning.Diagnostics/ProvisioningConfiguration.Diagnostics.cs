namespace EtAlii.Ubigia.Provisioning.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;

    public static class ProvisioningConfigurationDiagnosticsExtension
    {
        public static ProvisioningConfiguration Use(this ProvisioningConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IProvisioningExtension[]
            {
                new DiagnosticsProvisioningExtension(diagnostics), 
            };
            
            return configuration
                .Use(extensions)
                .Use((DataConnectionConfiguration c) => c.UseTransportDiagnostics(diagnostics))
                .Use((ManagementConnectionConfiguration c) => c.UseTransportDiagnostics(diagnostics))
                .Use((GraphSLScriptContextConfiguration c) => c.UseFunctionalGraphSLDiagnostics(diagnostics));
        }
    }
}