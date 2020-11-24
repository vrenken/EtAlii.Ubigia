namespace EtAlii.Ubigia.Provisioning.Hosting.ConsoleHost
{
	using System;
	using System.Threading.Tasks;
	using EtAlii.xTechnology.Diagnostics;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Hosting.Diagnostics;
	using global::Microsoft.Extensions.Configuration;

	public static class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        [STAThread]
        public static async Task Main()
        {
	        
	        var applicationConfigurationDetails = await new ConfigurationDetailsParser()
		        .Parse("settings.json").ConfigureAwait(false);

	        var applicationConfiguration = new ConfigurationBuilder()
		        .AddConfigurationDetails(applicationConfigurationDetails)
		        .Build();

	        var hostConfiguration = new HostConfigurationBuilder()
		        .Build(applicationConfiguration, applicationConfigurationDetails)
		        .Use(DiagnosticsConfiguration.Default)
		        .UseConsoleHost();

	        ConsoleHost.Start(hostConfiguration);
        }
    }
}
