namespace EtAlii.Ubigia.Provisioning.Hosting.ConsoleHost
{
	using System;
	using System.Threading.Tasks;
	using EtAlii.xTechnology.Hosting;
	using global::Microsoft.Extensions.Configuration;

	public static class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        [STAThread]
        public static async Task Main()
        {
	        
	        var details = await new ConfigurationDetailsParser()
		        .Parse("settings.json");

	        var applicationConfiguration = new ConfigurationBuilder()
		        .AddConfigurationDetails(details)
		        .Build();

	        var hostConfiguration = new HostConfigurationBuilder()
		        .Build(applicationConfiguration)
		        .UseConsoleHost();

	        ConsoleHost.Start(hostConfiguration);
        }
    }
}
