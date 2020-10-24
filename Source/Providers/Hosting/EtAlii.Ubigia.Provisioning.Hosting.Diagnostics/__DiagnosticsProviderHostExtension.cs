//namespace EtAlii.Ubigia.Provisioning.Hosting.Diagnostics
//[
//    using EtAlii.xTechnology.Diagnostics
//    using EtAlii.xTechnology.Logging
//    using EtAlii.xTechnology.MicroContainer

//    public class DiagnosticsProviderHostExtension : IHostExtension
//    [
//        private readonly IDiagnosticsConfiguration _diagnostics

//        internal DiagnosticsProviderHostExtension(IDiagnosticsConfiguration diagnostics)
//        [
//            _diagnostics = diagnostics
//        ]
//        public void Initialize(Container container)
//        [
//            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
//                () => new DisabledLogFactory(),
//                () => new DisabledProfilerFactory(),
//                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Provisioning"),
//                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Provisioning"))

//            var scaffoldings = new IScaffolding[]
//            [
//                new ProvisioningHostDiagnosticsScaffolding(diagnostics),
//                new ProvisioningHostDebuggingScaffolding(diagnostics),
//                new ProvisioningHostLoggingScaffolding(diagnostics),
//                new ProvisioningHostProfilingScaffolding(diagnostics),

//                //new WebApiProfilingScaffolding(diagnostics),
//                //new WebApiLoggingScaffolding(diagnostics),
//            ]
//            foreach (var scaffolding in scaffoldings)
//            [
//                scaffolding.Register(container)
//            ]
//        ]
//    ]
//]