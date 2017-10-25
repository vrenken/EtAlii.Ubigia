namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System;
    using System.Configuration;
    using System.Windows;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var startupDelay = e.Args.Length > 0 ? Int32.Parse(e.Args[0]) * 1000 : 0;
            System.Threading.Thread.Sleep(startupDelay);

            var name = "EtAlii";
            var category = "EtAlii.Ubigia.Provisioning";
            //var diagnostics = new DiagnosticsFactory().Create(true, false, true,
            //    () => new LogFactory(),
            //    () => new ProfilerFactory(),
            //    (factory) => factory.Create(name, category),
            //    (factory) => factory.Create(name, category));
            var diagnostics = new DiagnosticsFactory().CreateDisabled(name, category);

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
                .Use(diagnostics)
                .UseTrayIconHost();

            // And instantiate the host and start it.
            var host = new ProviderHostFactory<TrayIconHost>().Create(hostConfiguration);
            host.Start();
        }
    }
}
