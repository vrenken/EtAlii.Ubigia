//namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
//[
//    using EtAlii.xTechnology.Hosting
//    using EtAlii.xTechnology.MicroContainer
//    using Microsoft.Extensions.Configuration

//    public class AuthenticationServiceFactory : ServiceFactoryBase
//    [
//        public override IService Create(IConfigurationSection configuration)
//        [
//            var container = new Container()

//            container.Register<IAuthenticationService, AuthenticationService>()
//            container.Register<IConfigurationSection>(() => configuration)

//            return container.GetInstance<IAuthenticationService>()
//        ]
//    ]
//]