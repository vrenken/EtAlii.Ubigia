namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class StorageServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            return new StorageService();
        }
    }
}
