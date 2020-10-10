namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    using Microsoft.Extensions.Configuration;

    public class StorageService : ServiceBase
    {
        public StorageService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }
    }
}
