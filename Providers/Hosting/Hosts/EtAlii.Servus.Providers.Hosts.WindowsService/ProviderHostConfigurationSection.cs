namespace EtAlii.Servus.Provisioning.Hosting
{
    using System.Configuration;

    public class ProviderHostConfigurationSection : ConfigurationSection, IProviderHostConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"] as string; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password 
        {
            get { return this["password"] as string; }
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

        public IProviderHostConfiguration ToProviderHostConfiguration()
        {
            var configuration = new ProviderHostConfiguration()
                .Use(Address, Account, Password);
            return configuration;
        }
    }
}