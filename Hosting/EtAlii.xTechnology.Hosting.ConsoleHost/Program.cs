namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Configuration;

    public class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        public static void Start(IHostConfiguration configuration)
        {
            Console.WriteLine("Starting Ubigia infrastructure...");

            var host = new HostFactory<ConsoleHost>().Create(configuration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

            Console.WriteLine();
            Console.WriteLine("- Press any key to stop - ");
            Console.ReadKey();

            host.Stop();
        }
    }
}
