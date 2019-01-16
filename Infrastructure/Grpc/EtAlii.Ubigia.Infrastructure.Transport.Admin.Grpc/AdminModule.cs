namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc
{
    using EtAlii.xTechnology.Hosting.Grpc;
    using Microsoft.Extensions.Configuration;

    public class AdminModule : GrpcModuleBase
    {
        public AdminModule(IConfigurationSection configuration) 
            : base(configuration)
        {
        }
    }
}
