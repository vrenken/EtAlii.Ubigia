namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
    using Microsoft.Extensions.Configuration;

    public class InfrastructureService : ServiceBase
    {
        public InfrastructureService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }
    }
}
