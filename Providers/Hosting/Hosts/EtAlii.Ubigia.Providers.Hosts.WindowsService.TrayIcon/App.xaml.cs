namespace EtAlii.Ubigia.Provisioning.Hosting.TrayIconHost.Net47
{
    using System.Windows;
    using EtAlii.xTechnology.Hosting;
    using global::Microsoft.Extensions.Configuration;


	/// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
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
