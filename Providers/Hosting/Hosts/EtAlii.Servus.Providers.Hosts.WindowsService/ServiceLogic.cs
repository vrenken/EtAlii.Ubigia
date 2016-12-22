namespace EtAlii.Servus.Provisioning.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using EtAlii.xTechnology.Diagnostics;

    public class ServiceLogic : IServiceLogic
    {
        private const string _nameFormat = "SPS${0}"; // Servus Provider Service
        private const string _displayNameFormat = "Servus Provider Service ({0})";
        private const string _descriptionFormat = "Information exchange to and from the Servus storage '{0}'";
        

        public string Name { get { return _name; } }
        private readonly string _name;

        public string DisplayName { get { return _displayName; } }
        private readonly string _displayName;

        public string Description { get { return _description; } }
        private readonly string _description;
        
        private readonly IProviderHost _host;
        private readonly string _hostName;
        private readonly string _hostAddress;

        public ServiceLogic()
        {
            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Servus.Provisioning");

            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Let's first fetch the provider configurations.
            var providerConfigurationsSection = (IProviderConfigurationsSection)exeConfiguration.GetSection("servus/providers");
            var providerConfigurations = providerConfigurationsSection
                .ToProviderConfigurations();

            // And then the host configuration.
            var hostConfigurationSection = (IHostConfigurationSection)exeConfiguration.GetSection("servus/host");
            var hostConfiguration = hostConfigurationSection
                .ToHostConfiguration()
                .Use(providerConfigurations)
                .Use(diagnostics);

            // And finally the service configuration.
            var windowsServiceConfiguration = (WindowsServiceConfigurationSection)exeConfiguration.GetSection("servus/service");
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
            Console.WriteLine("Starting Servus infrastructure...");

            _host.Start();

            Console.WriteLine("All OK. Servus is serving the storage specified below.");
            Console.WriteLine("Name: " + _hostName);
            Console.WriteLine("Address: " + _hostAddress);
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
