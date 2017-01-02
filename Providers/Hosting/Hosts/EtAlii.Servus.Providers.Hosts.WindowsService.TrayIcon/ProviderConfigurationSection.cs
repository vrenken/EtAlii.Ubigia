namespace EtAlii.Servus.Provisioning.Hosting
{
    using System.Configuration;
    using EtAlii.Servus.Api;

    public class ProviderHostConfigurationSection : ConfigurationSection, IProviderHostConfigurationSection
    {
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

        public IProviderHostConfiguration ToProviderHostConfiguration()
        {
            var configuration = new ProviderHostConfiguration()
                .Use(Address, Account, Password);

            return configuration;
        }
    }
}