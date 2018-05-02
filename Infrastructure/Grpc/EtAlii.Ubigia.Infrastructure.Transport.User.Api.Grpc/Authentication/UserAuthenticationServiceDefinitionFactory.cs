namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Authentication
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

    public class UserAuthenticationServiceDefinitionFactory : IUserAuthenticationServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure)
        {
            var container = new Container();
            container.Register<IUserAuthenticationService, UserAuthenticationService>();
       
            new UserApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);
            
            var authenticationService= (UserAuthenticationService)container.GetInstance<IUserAuthenticationService>();
            return AuthenticationGrpcService.BindService(authenticationService);
        }
    }
}