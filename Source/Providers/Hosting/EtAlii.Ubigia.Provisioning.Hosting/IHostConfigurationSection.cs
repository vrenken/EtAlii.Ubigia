namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.Hosting;

    public interface IHostConfigurationSection
    {
        IHostConfiguration ToHostConfiguration();
    }
}