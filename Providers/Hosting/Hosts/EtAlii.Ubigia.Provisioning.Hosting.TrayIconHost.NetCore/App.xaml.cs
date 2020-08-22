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
	        var applicationConfigurationDetails = await new ConfigurationDetailsParser()
		        .Parse("settings.json");

	        var applicationConfiguration = new ConfigurationBuilder()
		        .AddConfigurationDetails(applicationConfigurationDetails)
		        .Build();

	        var hostConfiguration = new HostConfigurationBuilder()
		        .Build(applicationConfiguration, applicationConfigurationDetails);
            //     .UseTrayIconHost(
            //         this,
            //         "Icon-Logo-White-Shaded.ico",
            //         "Icon-Logo-Black.ico",
            //         "Icon-Logo-Red.ico");
            // TrayIconHost.Start(hostConfiguration);

            if (hostConfiguration != null)
            {
	            throw new InvalidOperationException("Tray icon host not implemented currently.");
            }
        }
	}
}
