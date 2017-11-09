namespace EtAlii.Ubigia.Infrastructure.Hosting.TrayIconHost
{
    using System.Configuration;
    using System.Windows;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.Ubigia.Infrastructure.Hosting.Owin;
    using EtAlii.xTechnology.Hosting;


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configuration = new HostConfigurationBuilder()
                .Build(sectionName => exeConfiguration.GetSection(sectionName))
                .UseTrayIconHost(
                    this,
                    "Icon-Logo-White-Shaded.ico",
                    "Icon-Logo-Black.ico",
                    "Icon-Logo-Red.ico");


        TrayIconHost.Start(configuration);
        }
    }
}
