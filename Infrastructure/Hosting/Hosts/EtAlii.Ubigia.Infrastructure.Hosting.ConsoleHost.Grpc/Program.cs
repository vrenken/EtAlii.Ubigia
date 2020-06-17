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
            // See: https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0#call-insecure-grpc-services-with-net-core-client
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

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
