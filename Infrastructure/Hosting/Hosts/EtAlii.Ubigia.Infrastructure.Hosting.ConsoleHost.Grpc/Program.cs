namespace EtAlii.Ubigia.Infrastructure.Hosting.ConsoleHost
{
    using Microsoft.Extensions.Configuration;
    using EtAlii.xTechnology.Hosting;


    public class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        public static void Main()
        {
            var applicationConfiguration = new ConfigurationBuilder()
                .AddJsonFile("settings.json", optional: true)
                //.AddXmlFile("app.config")
                .Build();

            var hostConfiguration = new HostConfigurationBuilder()
                .Build(applicationConfiguration)
                .UseConsoleHost();

            ConsoleHost.Start(hostConfiguration);
        }
    }
}
