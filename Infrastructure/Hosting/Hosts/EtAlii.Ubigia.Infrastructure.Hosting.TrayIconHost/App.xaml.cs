﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.TrayIconHost
{
    using System.Configuration;
    using System.Windows;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

	/// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
	        var applicationConfiguration = new ConfigurationBuilder()
		        .AddJsonFile("settings.json")
		        .Build();

	        var hostConfiguration = new HostConfigurationBuilder()
		        .Build(applicationConfiguration)
                .UseTrayIconHost(
                    this,
                    "Icon-Logo-White-Shaded.ico",
                    "Icon-Logo-Black.ico",
                    "Icon-Logo-Red.ico");

            TrayIconHost.Start(hostConfiguration);
        }
    }
}
