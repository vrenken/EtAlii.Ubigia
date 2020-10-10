namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public abstract class ServiceFactoryBase : IServiceFactory
    {
        public abstract IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails);
    }
}