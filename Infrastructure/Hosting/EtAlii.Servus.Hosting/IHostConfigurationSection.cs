namespace EtAlii.Servus.Infrastructure.Hosting
{
    public interface IHostConfigurationSection
    {
        IHostConfiguration ToHostConfiguration();
    }
}