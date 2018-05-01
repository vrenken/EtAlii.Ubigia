namespace EtAlii.Ubigia.Infrastructure.Transport.User.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class UserModule : GrpcModuleBase
    {
        public UserModule(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureServices(Server.ServiceDefinitionCollection serviceDefinitions)
        {
        }

    }
}
