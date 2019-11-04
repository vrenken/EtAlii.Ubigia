namespace EtAlii.Ubigia.Infrastructure.Hosting.ConsoleHost.Grpc
{
    using System;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.ConsoleHost;
    using Microsoft.Extensions.Configuration;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        public static void Main()
        {
            // See: https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0#call-insecure-grpc-services-with-net-core-client
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

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
