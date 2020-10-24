namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
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
