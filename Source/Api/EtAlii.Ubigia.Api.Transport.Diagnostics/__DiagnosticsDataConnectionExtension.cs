// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Api.Transport
//[
//    using EtAlii.xTechnology.Diagnostics
//    using EtAlii.xTechnology.Logging
//    using EtAlii.xTechnology.MicroContainer

//    public class DiagnosticsDataConnectionExtension : IDataConnectionExtension
//    [
//        private readonly IDiagnosticsConfiguration _diagnostics

//        internal DiagnosticsDataConnectionExtension(IDiagnosticsConfiguration diagnostics)
//        [
//            _diagnostics = diagnostics
//        ]
//        public void Initialize(Container container)
//        [
//            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
//                () => new DisabledLogFactory(),
//                () => new DisabledProfilerFactory(),
//                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"),
//                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"))

//            container.Register<IDiagnosticsConfiguration>(() => diagnostics)

//            var scaffoldings = new IScaffolding[]
//            [
//                new DataConnectionLoggingScaffolding(),
//                new DataConnectionProfilingScaffolding(),
//                new DataConnectionDebuggingScaffolding(),
//            ]
//            foreach (var scaffolding in scaffoldings)
//            [
//                scaffolding.Register(container)
//            ]
//        ]
//    ]
//]