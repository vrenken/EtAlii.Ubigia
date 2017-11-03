namespace EtAlii.Ubigia.Infrastructure.Hosting.TrayIconHost
{
    using System.Configuration;
    using System.Windows;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.xTechnology.Hosting;


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configuration = new HostConfigurationBuilder().Build(sectionName => exeConfiguration.GetSection(sectionName));

            TrayIconHost.Start(configuration, this);
        }
    }
}
