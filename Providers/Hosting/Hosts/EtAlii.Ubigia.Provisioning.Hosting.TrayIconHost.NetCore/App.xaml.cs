namespace EtAlii.Ubigia.Provisioning.Hosting.TrayIconHost
{
	using System.Windows;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Hosting.TrayIconHost;
	using global::Microsoft.Extensions.Configuration;

	partial class App
	{
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
	        var applicationConfiguration = new ConfigurationBuilder()
		        .AddJsonFile("settings.json")
		        .Build();

	        var hostConfiguration = new HostConfigurationBuilder()
		        .Build(applicationConfiguration)
                .UseTrayIconHost(
                    this,
                    "Icon-Logo-White-Shaded.ico",
                    "Icon-Logo-Black.ico",
                    "Icon-Logo-Red.ico");
            TrayIconHost.Start(hostConfiguration);
        }
	}
}
