namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

    public class AdminAuthenticationServiceDefinitionFactory : IAdminAuthenticationServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure)
        {
            var container = new Container();
            container.Register<IAdminAuthenticationService, AdminAuthenticationService>();
       
            new AuthenticationScaffolding(infrastructure).Register(container);     
            new SerializationScaffolding().Register(container);
            
            var authenticationService= (AdminAuthenticationService)container.GetInstance<IAdminAuthenticationService>();
            return AuthenticationGrpcService.BindService(authenticationService);
        }
    }
}