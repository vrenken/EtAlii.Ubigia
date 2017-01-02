namespace EtAlii.Ubigia.Provisioning.Hosting
{
    public interface IHostConfigurationSection
    {
        IHostConfiguration ToHostConfiguration();
    }
}