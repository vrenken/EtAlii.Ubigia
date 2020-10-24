namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public abstract class SystemFactoryBase : ISystemFactory
    {
        public abstract ISystem Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails);
    }
}