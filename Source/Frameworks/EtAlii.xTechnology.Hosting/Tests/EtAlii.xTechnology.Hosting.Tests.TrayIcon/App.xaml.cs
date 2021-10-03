// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.TrayIcon
{
	using System.Windows;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.xTechnology.Hosting.Diagnostics;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
	        var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("settings.json")
                .ExpandEnvironmentVariablesInJson()
		        .Build();

	        var hostOptions = new  HostOptions(configurationRoot)
                .UseTrayIconHost(this,
                    "Icon-Logo-White-Shaded.ico",
                    "Icon-Logo-Black.ico",
                    "Icon-Logo-Red.ico")
                .UseHostDiagnostics();

	        TrayIconHost.Start(hostOptions);
        }
    }
}
