namespace EtAlii.Ubigia.Provisioning
{
    using System.Configuration;

    public class ProvisioningConfigurationSection : ConfigurationSection, IProvisioningConfigurationSection
    {
        [ConfigurationProperty("password", IsRequired = false)]
        public string Password 
        {
            get { return this["password"] as string; }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("account", IsRequired = true)]
        public string Account => this["account"] as string;

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address => this["address"] as string;

        public IProvisioningConfiguration ToProvisioningConfiguration()
        {
            var configuration = new ProvisioningConfiguration()
                .Use(Address, Account, Password);

            return configuration;
        }
    }
}