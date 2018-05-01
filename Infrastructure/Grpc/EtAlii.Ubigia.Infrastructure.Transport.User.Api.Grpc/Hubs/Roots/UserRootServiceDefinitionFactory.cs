namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

    public class UserRootServiceDefinitionFactory : IUserRootServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure)
        {
            var container = new Container();
            container.Register<IUserRootService, UserRootService>();
       
            new AuthenticationScaffolding(infrastructure).Register(container);     
            new SerializationScaffolding().Register(container);
            
            var rootService = (UserRootService)container.GetInstance<IUserRootService>();
            return RootGrpcService.BindService(rootService);
        }
    }
}