namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class AdminModule : GrpcModuleBase
    {
        public AdminModule(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureServer(Server server)
        {
            base.OnConfigureServer(server);
        }

        protected override void OnConfigureServices(Server.ServiceDefinitionCollection serviceDefinitions)
        {
        }
    }
}
