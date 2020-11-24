namespace EtAlii.Ubigia.Provisioning.Hosting.TrayIconHost
{
	using System.Windows;
	using EtAlii.xTechnology.Diagnostics;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Hosting.Diagnostics;
	using global::Microsoft.Extensions.Configuration;

	partial class App
	{
        private async void OnApplicationStartup(object sender, StartupEventArgs e)
        {
	        var applicationConfigurationDetails = await new ConfigurationDetailsParser()
		        .Parse("settings.json").ConfigureAwait(false);

	        var applicationConfiguration = new ConfigurationBuilder()
		        .AddConfigurationDetails(applicationConfigurationDetails)
		        .Build();

	        var hostConfiguration = new HostConfigurationBuilder()
		        .Build(applicationConfiguration, applicationConfigurationDetails)
		        .Use(DiagnosticsConfiguration.Default)
                .UseTrayIconHost(
                    this,
                    "Icon-Logo-White-Shaded.ico",
                    "Icon-Logo-Black.ico",
                    "Icon-Logo-Red.ico");
	        
            TrayIconHost.Start(hostConfiguration);
        }
	}
}
