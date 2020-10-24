// namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
// {
//     using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
//     using EtAlii.Ubigia.Infrastructure.Functional;
//     using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
//     using EtAlii.xTechnology.MicroContainer;
//     using global::Grpc.Core;
//     using global::Grpc.Core.Interceptors;
//
//     public class UserContentServiceDefinitionFactory : IUserContentServiceDefinitionFactory
//     {
//         public ServerServiceDefinition Create(IInfrastructure infrastructure, ISpaceAuthenticationInterceptor spaceAuthenticationInterceptor)
//         {
//             var container = new Container();
//             container.Register<IUserContentService, UserContentService>();
//        
//             new UserApiScaffolding(infrastructure).Register(container);
//             new AuthenticationScaffolding().Register(container);     
//             new SerializationScaffolding().Register(container);
//             
//             var contentService = (UserContentService)container.GetInstance<IUserContentService>();
//             var serverServiceDefinition = ContentGrpcService.BindService(contentService);
//             return serverServiceDefinition.Intercept((Interceptor)spaceAuthenticationInterceptor);
//         }
//     }
// }