namespace EtAlii.Ubigia.Infrastructure.Hosting.ConsoleHost.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        public static async Task Main()
        {
            var details = await new ConfigurationDetailsParser().Parse("settings.json");

            var applicationConfiguration = new ConfigurationBuilder()
                .AddConfigurationDetails(details)
                .Build();
            
            var hostConfiguration = new HostConfigurationBuilder()
                .Build(applicationConfiguration, details)
                .UseConsoleHost();

            ConsoleHost.Start(hostConfiguration);
        }
    }
}
