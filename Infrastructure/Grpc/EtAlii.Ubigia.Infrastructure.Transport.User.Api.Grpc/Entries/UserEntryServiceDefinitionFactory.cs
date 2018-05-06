namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;
    using global::Grpc.Core.Interceptors;

    public class UserEntryServiceDefinitionFactory : IUserEntryServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure, ISpaceAuthenticationInterceptor spaceAuthenticationInterceptor)
        {
            var container = new Container();
            container.Register<IUserEntryService, UserEntryService>();
       
            new UserApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);
            
            var entryService = (UserEntryService)container.GetInstance<IUserEntryService>();
            var serverServiceDefinition = EntryGrpcService.BindService(entryService);
            return serverServiceDefinition.Intercept((Interceptor)spaceAuthenticationInterceptor);
        }
    }
}