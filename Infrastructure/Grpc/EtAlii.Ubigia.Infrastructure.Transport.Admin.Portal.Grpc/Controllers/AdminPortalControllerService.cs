namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Grpc
{
    using EtAlii.xTechnology.Hosting.Grpc;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalControllerService : GrpcServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureServices(Server.ServiceDefinitionCollection serviceDefinitions)
        {
            // Here we should implement the admin portal GRPC controller.
        }
    }
}
