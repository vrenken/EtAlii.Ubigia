namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    public interface IHostConfigurationSection
    {
        IHostConfiguration ToHostConfiguration();
    }
}