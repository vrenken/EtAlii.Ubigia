namespace EtAlii.xTechnology.Hosting
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class TrayIconHost
    {
        public static void Start(IHostConfiguration configuration, System.Windows.Application application)
        {
            application.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var host = new HostFactory<TrayIconHost>().Create(configuration);
            // Start hosting both the infrastructure and the storage.
            host.Start();
        }
    }
}