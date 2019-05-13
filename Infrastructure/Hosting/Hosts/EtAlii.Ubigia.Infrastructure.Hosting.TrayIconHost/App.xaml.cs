namespace EtAlii.Ubigia.Infrastructure.Hosting.TrayIconHost.NET47
{
	using System.Windows;
	using EtAlii.xTechnology.Hosting;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
//	        var applicationConfiguration = new ConfigurationBuilder()
//		        .AddJsonFile("settings.json")
//		        .Build()

	        var hostConfiguration = new HostConfiguration();// HostConfigurationBuilder()
		        //.Build(applicationConfiguration)
                //.UseTrayIconHost(
                //    this,
                //    "Icon-Logo-White-Shaded.ico",
                //    "Icon-Logo-Black.ico",
                //    "Icon-Logo-Red.ico")

            TrayIconHost.Start(hostConfiguration);
        }
    }
}
