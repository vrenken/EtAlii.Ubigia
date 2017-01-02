namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Configuration;

    public class HostConfigurationSection : ConfigurationSection, IHostConfigurationSection
    {
        public IHostConfiguration ToHostConfiguration()
        {
            var configuration = new HostConfiguration()
                .UseConsoleHost();
            return configuration;
            //.Use(infrastructure)
            //.Use(storage);
        }

        //public IHost Create(IInfrastructure infrastructure, IStorage storage)
        //{
        //    var configuration = new HostConfiguration()
        //        .UseConsoleHost()
        //        .Use(infrastructure)
        //        .Use(storage);
        //    return new HostFactory<ConsoleHost>().Create(configuration);
        //}

        //public IHost Create(IHostConfiguration configuration)
        //{
        //    return new HostFactory<ConsoleHost>().Create(configuration);
        //}
    }
}
