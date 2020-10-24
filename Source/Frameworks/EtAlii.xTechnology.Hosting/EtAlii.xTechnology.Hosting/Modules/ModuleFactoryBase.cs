namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public abstract class ModuleFactoryBase : IModuleFactory
    {
        public abstract IModule Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails);
    }
}