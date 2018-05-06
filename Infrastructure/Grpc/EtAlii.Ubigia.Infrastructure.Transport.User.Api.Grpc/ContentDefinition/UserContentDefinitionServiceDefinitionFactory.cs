namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;
    using global::Grpc.Core.Interceptors;

    public class UserContentDefinitionServiceDefinitionFactory : IUserContentDefinitionServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure, ISpaceAuthenticationInterceptor spaceAuthenticationInterceptor)
        {
            var container = new Container();
            container.Register<IUserContentDefinitionService, UserContentDefinitionService>();
       
            new UserApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);
            
            var contentDefinitionService = (UserContentDefinitionService)container.GetInstance<IUserContentDefinitionService>();
            var serverServiceDefinition = ContentDefinitionGrpcService.BindService(contentDefinitionService);
            return serverServiceDefinition.Intercept((Interceptor)spaceAuthenticationInterceptor);
        }
    }
}