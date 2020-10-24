namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public static class ProvisioningConfigurationUseExtensions
    {
        
        public static TProvisioningConfiguration Use<TProvisioningConfiguration>(this TProvisioningConfiguration configuration, ProviderConfiguration[] providerConfigurations)
            where TProvisioningConfiguration : ProvisioningConfiguration
        {
            var editableConfiguration = (IEditableProvisioningConfiguration) configuration;
            editableConfiguration.ProviderConfigurations = providerConfigurations ?? throw new ArgumentException(nameof(providerConfigurations));

            return configuration;
        }

        
        public static TProvisioningConfiguration Use<TProvisioningConfiguration>(this TProvisioningConfiguration configuration, Uri address, string account, string password)
            where TProvisioningConfiguration : ProvisioningConfiguration
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new ArgumentException(nameof(account));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(nameof(password));
            }

            var editableConfiguration = (IEditableProvisioningConfiguration) configuration;
            
            editableConfiguration.Address = address ?? throw new ArgumentNullException(nameof(address));
            editableConfiguration.Account = account;
            editableConfiguration.Password = password;
            return configuration;
        }
        
        
        public static TProvisioningConfiguration Use<TProvisioningConfiguration>(this TProvisioningConfiguration configuration, Action<DataConnectionConfiguration> dataConnectionConfigurationFactoryExtension)
            where TProvisioningConfiguration : ProvisioningConfiguration
        {
            if (dataConnectionConfigurationFactoryExtension == null)
            {
                throw new ArgumentException(nameof(dataConnectionConfigurationFactoryExtension));
            }

            var editableConfiguration = (IEditableProvisioningConfiguration) configuration;

            editableConfiguration.DataConnectionConfigurationFactoryExtensions = new[] { dataConnectionConfigurationFactoryExtension }
                .Concat(editableConfiguration.DataConnectionConfigurationFactoryExtensions)
                .Distinct()
                .ToArray();
            return configuration;
        }

        public static TProvisioningConfiguration Use<TProvisioningConfiguration>(this TProvisioningConfiguration configuration, Action<ManagementConnectionConfiguration> managementConnectionConfigurationFactoryExtension)
            where TProvisioningConfiguration : ProvisioningConfiguration
        {
            if (managementConnectionConfigurationFactoryExtension == null)
            {
                throw new ArgumentException(nameof(managementConnectionConfigurationFactoryExtension));
            }

            var editableConfiguration = (IEditableProvisioningConfiguration) configuration;

            editableConfiguration.ManagementConnectionConfigurationFactoryExtensions = new[] { managementConnectionConfigurationFactoryExtension }
                .Concat(editableConfiguration.ManagementConnectionConfigurationFactoryExtensions)
                .Distinct()
                .ToArray();
            return configuration;
        }

        public static TProvisioningConfiguration Use<TProvisioningConfiguration>(this TProvisioningConfiguration configuration, Action<GraphSLScriptContextConfiguration> scriptContextConfigurationFactoryExtension)
            where TProvisioningConfiguration : ProvisioningConfiguration
        {
            if (scriptContextConfigurationFactoryExtension == null)
            {
                throw new ArgumentException(nameof(scriptContextConfigurationFactoryExtension));
            }

            var editableConfiguration = (IEditableProvisioningConfiguration) configuration;

            editableConfiguration.ScriptContextConfigurationFactoryExtensions = new[] { scriptContextConfigurationFactoryExtension }
                .Concat(editableConfiguration.ScriptContextConfigurationFactoryExtensions)
                .Distinct()
                .ToArray();
            return configuration;
        }
        
        
        public static TProvisioningConfiguration Use<TProvisioningConfiguration>(this TProvisioningConfiguration configuration, Func<ITransportProvider> transportProviderFactory)
            where TProvisioningConfiguration : ProvisioningConfiguration
        {
            
            var editableConfiguration = (IEditableProvisioningConfiguration) configuration;

            editableConfiguration.TransportProviderFactory = transportProviderFactory ?? throw new ArgumentException(nameof(transportProviderFactory));
            return configuration;
        }

        public static TProvisioningConfiguration Use<TProvisioningConfiguration>(this TProvisioningConfiguration configuration, Func<IStorageTransportProvider> storageTransportProviderFactory)
            where TProvisioningConfiguration : ProvisioningConfiguration
        {
            
            var editableConfiguration = (IEditableProvisioningConfiguration) configuration;

            editableConfiguration.StorageTransportProviderFactory = storageTransportProviderFactory ?? throw new ArgumentException(nameof(storageTransportProviderFactory));
            return configuration;
        }


    }
}