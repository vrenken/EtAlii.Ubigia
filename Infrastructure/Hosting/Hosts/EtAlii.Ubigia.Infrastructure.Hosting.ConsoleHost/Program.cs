namespace EtAlii.Ubigia.Infrastructure.Hosting.ConsoleHost
{
    using System;
    using System.Configuration;
    using EtAlii.Ubigia.Infrastructure.Hosting;

    public class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Starting Ubigia infrastructure...");

            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configuration = new HostConfigurationBuilder().Build(sectionName => exeConfiguration.GetSection(sectionName));

            EtAlii.xTechnology.Hosting.Program.Start(configuration);
        }
    }
}
