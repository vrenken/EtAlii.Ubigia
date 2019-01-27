namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.Hosting.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class AdminGrpcService : GrpcServiceBase
    {
        public AdminGrpcService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureServices(Server.ServiceDefinitionCollection serviceDefinitions)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            var container = new Container();
            new AdminApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);

            container.Register<IAccountAuthenticationInterceptor, AccountAuthenticationInterceptor>();
            var accountAuthenticationInterceptor = container.GetInstance<IAccountAuthenticationInterceptor>();

            serviceDefinitions.Add(new AdminAuthenticationServiceDefinitionFactory().Create(infrastructure));
            serviceDefinitions.Add(new AdminStorageServiceDefinitionFactory().Create(infrastructure, accountAuthenticationInterceptor));
            serviceDefinitions.Add(new AdminAccountServiceDefinitionFactory().Create(infrastructure));
            serviceDefinitions.Add(new AdminSpaceServiceDefinitionFactory().Create(infrastructure));
        }
    }
}
