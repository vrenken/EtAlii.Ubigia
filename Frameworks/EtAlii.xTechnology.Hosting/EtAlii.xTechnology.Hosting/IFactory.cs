namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public interface IFactory<out TInstance>
    {
        TInstance Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails);
    }
}
