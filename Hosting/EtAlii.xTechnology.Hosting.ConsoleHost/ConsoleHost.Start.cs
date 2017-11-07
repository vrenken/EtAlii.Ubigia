namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class ConsoleHost
    {
        public static void Start(IHostConfiguration configuration)
        {
            Console.WriteLine("Starting Ubigia infrastructure...");

            var host = new HostFactory<ConsoleHost>().Create(configuration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

            var consoleDialog = new ConsoleDialog(host);
            consoleDialog.Start();
        }
    }
}