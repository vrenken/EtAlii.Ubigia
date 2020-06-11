namespace EtAlii.Ubigia.Provisioning.Hosting.TrayIconHost
{
	using System;
	using System.Windows;
	using EtAlii.xTechnology.Hosting;
	using global::Microsoft.Extensions.Configuration;

	partial class App
	{
        private async void OnApplicationStartup(object sender, StartupEventArgs e)
        {
	        var details = await new ConfigurationDetailsParser()
		        .Parse("settings.json");

	        var applicationConfiguration = new ConfigurationBuilder()
		        .AddConfigurationDetails(details)
		        .Build();

	        var hostConfiguration = new HostConfigurationBuilder()
		        .Build(applicationConfiguration);
            //     .UseTrayIconHost(
            //         this,
            //         "Icon-Logo-White-Shaded.ico",
            //         "Icon-Logo-Black.ico",
            //         "Icon-Logo-Red.ico");
            // TrayIconHost.Start(hostConfiguration);
            
            throw new InvalidOperationException("Tray icon host not implemented currently.");
        }
	}
}
