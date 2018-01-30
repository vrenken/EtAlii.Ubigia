namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System.Configuration;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public class InfrastructureConfigurationSection : ConfigurationSection, IInfrastructureConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address => this["address"] as string;

        public IInfrastructureConfiguration ToInfrastructureConfiguration()
        {
            var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            var configuration = new InfrastructureConfiguration(systemConnectionCreationProxy)
                .Use(Name, Address);
            return configuration;
        }
    }
}