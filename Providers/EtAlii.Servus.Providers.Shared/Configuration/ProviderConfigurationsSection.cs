namespace EtAlii.Servus.Provisioning
{
    using System.Configuration;
    using System.Collections.Generic;
    using System;

    public class ProviderConfigurationsSection : ConfigurationSection, IProviderConfigurationsSection
    {
        [ConfigurationProperty("", IsDefaultCollection  = true)]
        public ProviderCollection Providers
        {
            get
            {
                return (ProviderCollection) base[""];
            }
        }

        public IProviderConfiguration[] ToProviderConfigurations()
        {
            var configurations = new List<IProviderConfiguration>();

            foreach (ProviderElement provider in Providers)
            {
                var type = Type.GetType(provider.Type, true);
                var providerFactory = (IProviderFactory)Activator.CreateInstance(type);
                var configuration = new ProviderConfiguration()
                    .Use(providerFactory);
                configurations.Add(configuration);
            }

            return configurations.ToArray();
        }

    }
}