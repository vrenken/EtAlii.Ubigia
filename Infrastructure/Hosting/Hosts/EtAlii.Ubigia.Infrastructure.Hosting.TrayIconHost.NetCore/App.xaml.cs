namespace EtAlii.Ubigia.Infrastructure.Hosting.TrayIconHost
{
	using System.Windows;
	using EtAlii.xTechnology.Hosting;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
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
                //    "Icon-Logo-White-Shaded.ico",
                //    "Icon-Logo-Black.ico",
                //    "Icon-Logo-Red.ico")

            //TrayIconHost.Start(hostConfiguration);
        }
    }
}
