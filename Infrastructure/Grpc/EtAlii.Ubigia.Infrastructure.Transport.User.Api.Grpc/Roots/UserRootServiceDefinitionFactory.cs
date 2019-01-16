namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;
    using global::Grpc.Core.Interceptors;

    public class UserRootServiceDefinitionFactory : IUserRootServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure, ISpaceAuthenticationInterceptor spaceAuthenticationInterceptor)
        {
            var container = new Container();
            container.Register<IUserRootService, UserRootService>();
       
            new UserApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);
            
            var rootService = (UserRootService)container.GetInstance<IUserRootService>();
            var serverServiceDefinition = RootGrpcService.BindService(rootService);
            return serverServiceDefinition.Intercept((Interceptor)spaceAuthenticationInterceptor);
        }
    }
}