// namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
// {
//     using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
//     using EtAlii.Ubigia.Infrastructure.Functional;
//     using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
//     using EtAlii.xTechnology.MicroContainer;
//     using global::Grpc.Core;
//     using global::Grpc.Core.Interceptors;
//
//     public class UserAccountServiceDefinitionFactory : IUserAccountServiceDefinitionFactory
//     {
//         public ServerServiceDefinition Create(IInfrastructure infrastructure, IAccountAuthenticationInterceptor accountAuthenticationInterceptor)
//         {
//             var container = new Container();
//             container.Register<IUserAccountService, UserAccountService>();
//        
//             new UserApiScaffolding(infrastructure).Register(container);
//             new AuthenticationScaffolding().Register(container);     
//             new SerializationScaffolding().Register(container);
//             
//             var accountService = (UserAccountService)container.GetInstance<IUserAccountService>();
//             var serverServiceDefinition = AccountGrpcService.BindService(accountService);
//             return serverServiceDefinition.Intercept((Interceptor)accountAuthenticationInterceptor);
//         }
//     }
// }