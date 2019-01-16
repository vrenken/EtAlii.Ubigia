namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;
    using global::Grpc.Core.Interceptors;

    public class AdminStorageServiceDefinitionFactory : IAdminStorageServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure, IAccountAuthenticationInterceptor accountAuthenticationInterceptor)
        {
            var container = new Container();
            container.Register<IAdminStorageService, AdminStorageService>();
       
            new AdminApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);
            
            var storageService = (AdminStorageService)container.GetInstance<IAdminStorageService>();
            var serverServiceDefinition = StorageGrpcService.BindService(storageService);
            return serverServiceDefinition.Intercept((Interceptor)accountAuthenticationInterceptor);
        }
    }
}