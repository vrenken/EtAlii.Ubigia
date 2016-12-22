namespace EtAlii.Servus.Provisioning.Hosting
{
    public interface IHostConfigurationSection
    {
        IHostConfiguration ToHostConfiguration();
    }
}