﻿//namespace EtAlii.Ubigia.Provisioning.Hosting
//[
//    using System
//    using System.Configuration
//    using System.Diagnostics
//    using System.Threading
//    using EtAlii.xTechnology.Diagnostics
//    using EtAlii.xTechnology.Logging


//    public class Program2
//    [
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        public static void Main(string[] args)
//        [
//            //var startupDelay = args.Length > 0 ? int.Parse(args[0]) * 1000 : 0
//            //System.Threading.Thread.Sleep(startupDelay)

//            Console.WriteLine("Starting Ubigia provider...")


//            // TODO: Should be removed.
//            if [Debugger.IsAttached]
//            [
//                Thread.Sleep(5000)
//            ]
//            //var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Provisioning")
//            var diagnostics = new DiagnosticsFactory().Create<DebugLogFactory, DisabledProfilerFactory>(true, false, true, "EtAlii", "EtAlii.Ubigia.Provisioning")

//            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

//            // Let's first fetch the provider configurations.
//            var providerConfigurationsSection = (IProviderConfigurationsSection)exeConfiguration.GetSection("ubigia/providers")
//            var providerConfigurations = providerConfigurationsSection
//                .ToProviderConfigurations()

//            // And then the host configuration.
//            var hostConfigurationSection = (IHostConfigurationSection)exeConfiguration.GetSection("ubigia/host")
//            var hostConfiguration = hostConfigurationSection
//                .ToHostConfiguration()
//                .Use(providerConfigurations)
//                .Use(diagnostics)
//                .UseConsoleHost()

//            // And instantiate the host and start it.
//            var host = new ProviderHostFactory<ConsoleHost>().Create(hostConfiguration)
//            host.Start()

//            Console.WriteLine("All OK. Ubigia is providing the storage specified below.")
//            Console.WriteLine("Address: " + hostConfiguration.Address)
//            Console.WriteLine()
//            Console.WriteLine("- Press any key to stop - ")
//            Console.ReadKey()

//            host.Stop()
//        ]
//    ]
//]