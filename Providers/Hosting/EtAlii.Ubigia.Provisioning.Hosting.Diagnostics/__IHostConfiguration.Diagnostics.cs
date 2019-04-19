//namespace EtAlii.Ubigia.Provisioning.Hosting.Diagnostics
//[
//    using EtAlii.Ubigia.Api.Transport.Diagnostics
//    using EtAlii.Ubigia.Api.Functional
//    using EtAlii.Ubigia.Api.Functional.Diagnostics
//    using EtAlii.Ubigia.Api.Transport
//    using EtAlii.Ubigia.Api.Transport.Management
//    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics
//    using EtAlii.xTechnology.Diagnostics

//    public static class IHostConfigurationDiagnosticsExtension
//    [
//        public static IHostConfiguration Use(this IHostConfiguration configuration, IDiagnosticsConfiguration diagnostics)
//        [
//            var extensions = new IHostExtension[]
//            [
//                new DiagnosticsProviderHostExtension(diagnostics), 
//            ]
//            return configuration
//                .Use(extensions)
//                .Use((IDataConnectionConfiguration c) => c.Use(diagnostics))
//                .Use((IManagementConnectionConfiguration c) => c.Use(diagnostics))
//                .Use((IDataContextConfiguration c) => c.Use(diagnostics))
//        ]
//    ]
//]