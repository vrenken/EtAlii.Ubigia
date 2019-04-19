//namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.Tests
//[
//    using EtAlii.Ubigia.Infrastructure.Functional
//    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
//    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
//    using EtAlii.xTechnology.Diagnostics
//    using Microsoft.AspNet.SignalR

//    public static class IInfrastructureConfigurationTestExtension
//    [
//        private static readonly SignalRComponentManagerFactory _signalRFactory = new SignalRComponentManagerFactory()
//        private static readonly WebApiComponentManagerFactory _webApiFactory = new WebApiComponentManagerFactory()

//        public static IInfrastructureConfiguration UseSignalRTestApi(this IInfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics)
//        [
//            var signalRDependencyResolver = new DefaultDependencyResolver()

//            var extensions = new IInfrastructureExtension[]
//            [
//                new TestInfrastructureExtension(diagnostics, signalRDependencyResolver), 
//            }
//            return configuration
//                .Use(extensions)
//                .Use(_webApiFactory.Create)
//                .Use((container, components) => _signalRFactory.Create(signalRDependencyResolver, components))
//        }
//    }
//}