namespace EtAlii.Servus.Infrastructure.Hosting.WebApi
{
    using EtAlii.Servus.Infrastructure;
    using System.Configuration;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class InfrastructureConfigurationSection : ConfigurationSection, IInfrastructureConfiguration, IInfrastructureFactory
    {
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = false)]
        public string Password 
        {
            get { return this["password"] as string; }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("account", IsRequired = true)]
        public string Account
        {
            get
            {
                return this["account"] as string;
            }
        }

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get
            {
                return this["address"] as string;
            }
        }

        public IInfrastructure Create(IStorage storage, IDiagnosticsConfiguration diagnostics)
        {
            return new WebApiInfrastructureFactory(this).Create(storage, diagnostics);
        }
    }
}