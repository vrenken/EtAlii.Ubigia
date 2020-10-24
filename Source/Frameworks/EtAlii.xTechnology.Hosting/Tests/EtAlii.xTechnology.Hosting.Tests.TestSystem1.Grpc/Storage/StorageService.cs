namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
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
