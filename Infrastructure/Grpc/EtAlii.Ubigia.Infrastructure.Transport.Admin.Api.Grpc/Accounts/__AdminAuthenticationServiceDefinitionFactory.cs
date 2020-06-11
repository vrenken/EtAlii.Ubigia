// namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
// {
//     using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
//     using EtAlii.Ubigia.Infrastructure.Functional;
//     using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
//     using EtAlii.xTechnology.MicroContainer;
//     using global::Grpc.Core;
//
//     public class AdminAccountServiceDefinitionFactory : IAdminAccountServiceDefinitionFactory
//     {
//         public ServerServiceDefinition Create(IInfrastructure infrastructure)
//         {
//             var container = new Container();
//             container.Register<IAdminAccountService, AdminAccountService>();
//        
//             new AdminApiScaffolding(infrastructure).Register(container);
//             new AuthenticationScaffolding().Register(container);     
//             new SerializationScaffolding().Register(container);
//             
//             var authenticationService = (AdminAccountService)container.GetInstance<IAdminAccountService>();
//             return AccountGrpcService.BindService(authenticationService);
//         }
//     }
// }