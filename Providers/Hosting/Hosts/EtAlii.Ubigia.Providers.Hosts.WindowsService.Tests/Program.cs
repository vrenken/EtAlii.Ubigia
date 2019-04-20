namespace EtAlii.Ubigia.Provisioning.Hosting.ConsoleHost.Net47
{
	using System;
	using EtAlii.xTechnology.Hosting;
	using global::Microsoft.Extensions.Configuration;

	public static class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        [STAThread]
        public static void Main()
        {
	        var applicationConfiguration = new ConfigurationBuilder()
		        .AddJsonFile("settings.json")
		        .Build();

	        var hostConfiguration = new HostConfigurationBuilder()
		        .Build(applicationConfiguration)
		        .UseConsoleHost();

	        ConsoleHost.Start(hostConfiguration);
        }
    }
}
