namespace EtAlii.xTechnology.Hosting
{
    public interface IHostConfigurationSection
    {
        IHostConfiguration ToHostConfiguration();
    }
}