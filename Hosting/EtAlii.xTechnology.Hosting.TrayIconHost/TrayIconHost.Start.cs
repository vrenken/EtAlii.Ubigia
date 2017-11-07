namespace EtAlii.xTechnology.Hosting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class TrayIconHost
    {
        public static void Start(IHostConfiguration configuration)
        {
            var host = new HostFactory<TrayIconHost>().Create(configuration);
            // Start hosting both the infrastructure and the storage.
            host.Start();
        }
    }
}