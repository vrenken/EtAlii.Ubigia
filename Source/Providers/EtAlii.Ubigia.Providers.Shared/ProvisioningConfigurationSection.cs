namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Configuration;

    public class ProvisioningConfigurationSection : ConfigurationSection, IProvisioningConfigurationSection
    {
        [ConfigurationProperty("password", IsRequired = false)]
        public string Password { get => this["password"] as string; set => this["password"] = value; }

        [ConfigurationProperty("account", IsRequired = true)]
        public string Account => this["account"] as string;

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address => this["address"] as string;

        public IProvisioningConfiguration ToProvisioningConfiguration()
        {
			var address = new Uri(Address, UriKind.Absolute);
            var configuration = new ProvisioningConfiguration()
                .Use(address, Account, Password);

            return configuration;
        }
    }
}