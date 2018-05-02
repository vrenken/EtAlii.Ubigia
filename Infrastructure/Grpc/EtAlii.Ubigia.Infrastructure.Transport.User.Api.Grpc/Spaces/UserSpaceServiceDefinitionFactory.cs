namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Spaces
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

    public class UserSpaceServiceDefinitionFactory : IUserSpaceServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure)
        {
            var container = new Container();
            container.Register<IUserSpaceService, UserSpaceService>();
       
            new UserApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);
            
            var spaceService = (UserSpaceService)container.GetInstance<IUserSpaceService>();
            return SpaceGrpcService.BindService(spaceService);
        }
    }
}