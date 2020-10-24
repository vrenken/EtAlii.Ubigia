namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public static class ProviderConfigurationUseExtensions
    {
        public static TProviderConfiguration Use<TProviderConfiguration>(this TProviderConfiguration configuration, IProviderFactory factory)
            where TProviderConfiguration : ProviderConfiguration
        {
            var editableConfiguration = (IEditableProviderConfiguration) configuration;
            
            editableConfiguration.Factory = factory ?? throw new ArgumentException(nameof(factory));

            return configuration;
        }

        public static TProviderConfiguration Use<TProviderConfiguration>(this TProviderConfiguration configuration, IManagementConnection managementConnection)
            where TProviderConfiguration : ProviderConfiguration
        {
            var editableConfiguration = (IEditableProviderConfiguration) configuration;
            
            editableConfiguration.ManagementConnection = managementConnection ?? throw new ArgumentException(nameof(managementConnection));

            return configuration;
        }

        public static TProviderConfiguration Use<TProviderConfiguration>(this TProviderConfiguration configuration, IGraphSLScriptContext systemScriptContext)
            where TProviderConfiguration : ProviderConfiguration
        {
            var editableConfiguration = (IEditableProviderConfiguration) configuration;
            
            editableConfiguration.SystemScriptContext = systemScriptContext ?? throw new ArgumentException(nameof(systemScriptContext));

            return configuration;
        }

        public static TProviderConfiguration Use<TProviderConfiguration>(this TProviderConfiguration configuration, Func<IDataConnection, IGraphSLScriptContext> scriptContextFactory)
            where TProviderConfiguration : ProviderConfiguration
        {
            var editableConfiguration = (IEditableProviderConfiguration) configuration;

            editableConfiguration.ScriptContextFactory = scriptContextFactory ?? throw new ArgumentException(nameof(scriptContextFactory));

            return configuration;
        }
    }
}