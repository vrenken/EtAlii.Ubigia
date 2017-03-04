namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using EtAlii.xTechnology.Diagnostics;

    public class ServiceLogic : IServiceLogic
    {
        private const string _nameFormat = "SPS${0}"; // Ubigia Provider Service
        private const string _displayNameFormat = "Ubigia Provider Service ({0})";
        private const string _descriptionFormat = "Information exchange to and from the Ubigia storage '{0}'";
        

        public string Name => _name;
        private readonly string _name;

        public string DisplayName => _displayName;
        private readonly string _displayName;

        public string Description => _description;
        private readonly string _description;
        
        private readonly IProviderHost _host;
        private readonly string _hostName;
        private readonly string _hostAddress;

        public ServiceLogic()
        {
            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Provisioning");

            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Let's first fetch the provider configurations.
            var providerConfigurationsSection = (IProviderConfigurationsSection)exeConfiguration.GetSection("ubigia/providers");
            var providerConfigurations = providerConfigurationsSection
                .ToProviderConfigurations();

            // And then the host configuration.
            var hostConfigurationSection = (IHostConfigurationSection)exeConfiguration.GetSection("ubigia/host");
            var hostConfiguration = hostConfigurationSection
                .ToHostConfiguration()
                .Use(providerConfigurations)
                .Use(diagnostics);

            // And finally the service configuration.
            var windowsServiceConfiguration = (WindowsServiceConfigurationSection)exeConfiguration.GetSection("ubigia/service");
            _hostName = windowsServiceConfiguration.Name;
            _hostAddress = hostConfiguration.Address;
            _name = String.Format(_nameFormat, _hostName).Replace(" ", "_");
            _displayName = String.Format(_displayNameFormat, _hostName);
            _description = String.Format(_descriptionFormat, _hostName);

            // And instantiate the host.
            _host = new ProviderHostFactory<WindowsServiceHost>().Create(hostConfiguration);
        }

        public void Start(IEnumerable<string> args)
        {
            Console.WriteLine("Starting Ubigia infrastructure...");

            _host.Start();

            Console.WriteLine("All OK. Ubigia is serving the storage specified below.");
            Console.WriteLine("Name: " + _hostName);
            Console.WriteLine("Address: " + _hostAddress);
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
