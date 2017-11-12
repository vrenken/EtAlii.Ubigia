namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using EtAlii.xTechnology.Hosting;

    public interface IHostConfigurationSection
    {
        IHostConfiguration ToHostConfiguration();
    }
}