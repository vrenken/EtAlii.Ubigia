namespace EtAlii.Ubigia.Infrastructure.Hosting.ConsoleHost.NetCore
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
            var applicationConfigurationDetails = await new ConfigurationDetailsParser()
                .Parse("settings.json");

            var applicationConfiguration = new ConfigurationBuilder()
                .AddConfigurationDetails(applicationConfigurationDetails)
                .Build();

            var hostConfiguration = new HostConfigurationBuilder()
                .Build(applicationConfiguration, applicationConfigurationDetails)
                .UseConsoleHost();

            ConsoleHost.Start(hostConfiguration);
        }
    }
}
